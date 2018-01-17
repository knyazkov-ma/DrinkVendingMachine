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
        decimal GetShoppingСartTotalPayment();
        decimal GetShoppingСartTotalCost();
        IEnumerable<SurrenderCoinDTO> Purchase();
    }
}