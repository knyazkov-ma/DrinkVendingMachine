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
                dbContext.SelectedDrinks.Add(selectedDrink);
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

        public IEnumerable<CoinDTO> GetCoins()
        {
            return dbContext.Coins
                .Select(c => new CoinDTO
                {
                    Id = c.Id,
                    Denomination = (int)c.Denomination,
                    Lock = c.Lock
                })
                .OrderBy(c => c.Denomination)
                .ToList();
        }

        public IEnumerable<ShowcaseDrinkDTO> GetShowcaseDrinks()
        {
            decimal shoppingСartTotalPayment = GetShoppingСartTotalPayment();

            IEnumerable<ShowcaseDrinkDTO> drinks =
                (from d in dbContext.Drinks.OrderBy(t => t.Ord)
                 join b in dbContext.DrinkBalances on d.Id equals b.DrinkId
                 join s in dbContext.SelectedDrinks on d.Id equals s.DrinkId into bs
                 from p in bs.DefaultIfEmpty()
                 select new
                 ShowcaseDrinkDTO
                 {
                     Id = d.Id,
                     Cost = d.Cost,
                     Count = b.Count,
                     Selected = p != null,
                     CanbeSelected = d.Cost <= shoppingСartTotalPayment
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

        public IEnumerable<SurrenderCoinDTO> GetSurrender(int surrender, IEnumerable<int> notLockCoins)
        {
            IList<SurrenderCoinDTO> surrenderCoins = new List<SurrenderCoinDTO>();

            //размен сделаем жадным способом
            int tail = 0;
            int head = surrender;
            foreach (var denomination in notLockCoins)
            {
                int mod = head % denomination;

                tail = head - mod;
                surrenderCoins.Add(new SurrenderCoinDTO
                {
                    Count = tail / denomination,
                    Denomination = denomination
                });

                if (mod == 0)
                    return surrenderCoins;

                head = tail;
            }

            return surrenderCoins;
        }

        public IEnumerable<SurrenderCoinDTO> GetSurrender()
        {
            int shoppingСartTotalPayment = GetShoppingСartTotalPayment();
            int shoppingСartTotalCost = (int)Math.Round(GetShoppingСartTotalCost() * 100); //в коп.;
            int surrender = shoppingСartTotalPayment - shoppingСartTotalCost;

            IEnumerable<int> notLockCoins = dbContext.Coins
                    .Where(t => !t.Lock)
                    .OrderByDescending(t => t.Denomination)
                    .Select(t => (int)t.Denomination)
                    .ToList();

            return GetSurrender(surrender, notLockCoins);
        }
        [Transaction]
        public void Purchase()
        {
            int shoppingСartTotalPayment = GetShoppingСartTotalPayment();
            int shoppingСartTotalCost = (int)Math.Round(GetShoppingСartTotalCost() * 100); //в коп.;
            int surrender = shoppingСartTotalPayment - shoppingСartTotalCost;

            foreach (var s in dbContext.SelectedDrinks)
            {
                var d = dbContext.DrinkBalances.FirstOrDefault(r => r.DrinkId == s.DrinkId);
                d.Count--;
            }
                        
            if (surrender > 0)
            {
                IEnumerable<Coin> notLockCoins = dbContext.Coins
                    .Where(t => !t.Lock)
                    .OrderByDescending(t => t.Denomination)
                    .ToList();

                var surrenderCoins = GetSurrender(surrender, notLockCoins.Select(t => (int)t.Denomination));

                foreach (var c in notLockCoins)
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