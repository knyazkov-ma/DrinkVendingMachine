using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Entity;
using System.Collections.Generic;

namespace DrinkVendingMachine.DataService.Interface
{
    public interface IShoppingСartService
    {
        void Init();
        void SelectDrink(long drinkId);
        void Pay(long coinId);
        IEnumerable<ShowcaseCoinDTO> GetCoins();
        IEnumerable<ShowcaseDrinkDTO> GetDrinks();
        int GetShoppingСartTotalPayment();
        decimal GetShoppingСartTotalCost();
        bool GetSurrender(int surrender, IEnumerable<Coin> coins, ref IList<SurrenderCoinDTO> surrenderCoins);
        bool GetSurrender(ref IList<SurrenderCoinDTO> surrenderCoins);
        void Purchase();        
    }
}