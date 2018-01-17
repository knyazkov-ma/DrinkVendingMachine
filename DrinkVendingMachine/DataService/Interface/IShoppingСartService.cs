using DrinkVendingMachine.DTO;
using System.Collections.Generic;

namespace DrinkVendingMachine.DataService.Interface
{
    public interface IShoppingСartService
    {
        void Init();
        void SelectDrink(long drinkId);
        void Pay(long coinId);
        IEnumerable<CoinDTO> GetCoins();
        IEnumerable<ShowcaseDrinkDTO> GetShowcaseDrinks();
        int GetShoppingСartTotalPayment();
        decimal GetShoppingСartTotalCost();
        IEnumerable<SurrenderCoinDTO> GetSurrender(int surrender, IEnumerable<int> notLockCoins);
        IEnumerable<SurrenderCoinDTO> GetSurrender();
        void Purchase();        
    }
}