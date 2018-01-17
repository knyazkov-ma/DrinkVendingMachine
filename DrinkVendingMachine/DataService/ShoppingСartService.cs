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

        public decimal GetShoppingСartTotalPayment()
        {
            return dbContext.PaymentCoins.Sum(p => (int)p.Coin.Denomination * p.Count);
        }

        public decimal GetShoppingСartTotalCost()
        {
            return
                 (from d in dbContext.Drinks
                  join s in dbContext.SelectedDrinks on d.Id equals s.DrinkId
                  select d.Cost).Sum();

        }

        [Transaction]
        public IEnumerable<SurrenderCoinDTO> Purchase()
        {
            decimal shoppingСartTotalPayment = GetShoppingСartTotalPayment();
            decimal shoppingСartTotalCost = GetShoppingСartTotalCost();

            foreach (var s in dbContext.SelectedDrinks)
            {
                var d = dbContext.DrinkBalances.FirstOrDefault(r => r.DrinkId == s.DrinkId);
                d.Count--;
            }

            decimal diff = shoppingСartTotalPayment - shoppingСartTotalCost;
            if (diff == 0)
                return null;

            IList<SurrenderCoinDTO> surrenderCoins = new List<SurrenderCoinDTO>();
            //размен сделаем жадным способом
            foreach (var c in dbContext.Coins)
            {

            }

            dbContext.SaveChanges();

            return surrenderCoins;
        }
    }
}