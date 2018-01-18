using DrinkVendingMachine.Aspects;
using DrinkVendingMachine.DAL;
using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkVendingMachine.DataService
{
    [Transaction]
    public class ShoppingСartService : IShoppingСartService
    {
        private readonly DbContext dbContext;
        public ShoppingСartService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Init()
        {
            dbContext.Database.ExecuteSqlCommand("delete from PaymentCoin");
            dbContext.Database.ExecuteSqlCommand("delete from SelectedDrink");
        }

        public void SelectDrink(long drinkId)
        {
            var selectedDrink = dbContext.SelectedDrinks.FirstOrDefault(d => d.DrinkId == drinkId);
            if (selectedDrink == null)
            {
                selectedDrink = new SelectedDrink { DrinkId = drinkId };
                dbContext.SelectedDrinks.Add(selectedDrink);
            }
            else
                dbContext.SelectedDrinks.Remove(selectedDrink);

            dbContext.SaveChanges();
        }

        public void Pay(long coinId)
        {
            PaymentCoin paymentCoin = dbContext.PaymentCoins.FirstOrDefault(p => p.Coin.Id == coinId);

            if (paymentCoin == null)
            {
                var coin = dbContext.Coins.FirstOrDefault(c => c.Id == coinId);
                paymentCoin = new PaymentCoin { Coin = coin, Count = 1 };
                dbContext.PaymentCoins.Add(paymentCoin);
            }
            else
                paymentCoin.Count++;

            dbContext.SaveChanges();
        }

        public IEnumerable<ShowcaseCoinDTO> GetCoins()
        {
            return dbContext.Coins
                .Select(c => new ShowcaseCoinDTO
                {
                    Id = c.Id,
                    Denomination = (int)c.Denomination,
                    Lock = c.Lock
                })
                .OrderBy(c => c.Denomination)
                .ToList();
        }

        public IEnumerable<ShowcaseDrinkDTO> GetDrinks()
        {
            int shoppingСartTotalPayment = GetShoppingСartTotalPayment();
            int shoppingСartTotalCost = (int)Math.Round(GetShoppingСartTotalCost() * 100); //в коп.;

            int diff = shoppingСartTotalPayment - shoppingСartTotalCost;

            IEnumerable<ShowcaseDrinkDTO> drinks =
                (from d in dbContext.Drinks.Where(t => t.Count > 0).OrderBy(t => t.Ord)
                 join s in dbContext.SelectedDrinks on d.Id equals s.DrinkId into bs
                 from p in bs.DefaultIfEmpty()
                 select new
                 ShowcaseDrinkDTO
                 {
                     Id = d.Id,
                     Cost = d.Cost,
                     Count = d.Count,
                     Selected = p != null,
                     CanbeSelected = (p == null && d.Cost * 100 <= diff)
                 }).ToList();

            return drinks;

        }

        public int GetShoppingСartTotalPayment()
        {
            return dbContext.PaymentCoins.Sum(p => (int?)p.Coin.Denomination * p.Count) ?? 0;
        }

        public decimal GetShoppingСartTotalCost()
        {
            return
                 (from d in dbContext.Drinks
                  join s in dbContext.SelectedDrinks on d.Id equals s.DrinkId
                  select (decimal?)d.Cost).Sum() ?? 0;

        }

        public IEnumerable<SurrenderCoinDTO> GetSurrender(int surrender, IEnumerable<int> coins)
        {
            IList<SurrenderCoinDTO> surrenderCoins = new List<SurrenderCoinDTO>();

            if (surrender == 0)
                return surrenderCoins;

            //размен сделаем жадным способом
            int tail = 0;
            int head = surrender;
            foreach (var denomination in coins)
            {
                int mod = head % denomination;

                tail = head - mod;
                int count = tail / denomination;
                if(count > 0)
                    surrenderCoins.Add(new SurrenderCoinDTO
                    {
                        Count = count,
                        Denomination = denomination
                    });

                if (mod == 0)
                    return surrenderCoins;

                head = mod;
            }

            return surrenderCoins;
        }

        public IEnumerable<SurrenderCoinDTO> GetSurrender()
        {
            int shoppingСartTotalPayment = GetShoppingСartTotalPayment();
            int shoppingСartTotalCost = (int)Math.Round(GetShoppingСartTotalCost() * 100); //в коп.;

            if(shoppingСartTotalCost == 0)
                return new List<SurrenderCoinDTO>();

            int surrender = shoppingСartTotalPayment - shoppingСartTotalCost;

            IEnumerable<int> coins = dbContext.Coins
                    .OrderByDescending(t => t.Denomination)
                    .Select(t => (int)t.Denomination)
                    .ToList();

            return GetSurrender(surrender, coins);
        }
        [Transaction]
        public void Purchase()
        {
            int shoppingСartTotalPayment = GetShoppingСartTotalPayment();
            int shoppingСartTotalCost = (int)Math.Round(GetShoppingСartTotalCost() * 100); //в коп.;
            int surrender = shoppingСartTotalPayment - shoppingСartTotalCost;

            foreach (var s in dbContext.SelectedDrinks.ToList())
            {
                var d = dbContext.Drinks.FirstOrDefault(r => r.Id == s.DrinkId);
                d.Count--;
            }
                        
            if (surrender > 0)
            {
                IEnumerable<Coin> coins = dbContext.Coins
                    .OrderByDescending(t => t.Denomination)
                    .ToList();

                var surrenderCoins = GetSurrender(surrender, coins.Select(t => (int)t.Denomination));

                foreach (var c in coins)
                {
                    SurrenderCoinDTO surrenderCoin = surrenderCoins.FirstOrDefault(t => t.Denomination == (int)c.Denomination);
                    if (surrenderCoin != null)
                        c.Count--;
                }
            }

            dbContext.SaveChanges();
        }

        public byte[] GetImageBody(long drinkId)
        {
            return dbContext.Drinks
                .Where(t => t.Id == drinkId)
                .Select(t => t.Image)
                .FirstOrDefault();
        }
    }
}