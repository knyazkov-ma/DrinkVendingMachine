using DrinkVendingMachine.DTO;
using System.Collections.Generic;

namespace DrinkVendingMachine.DataService.Interface
{
    public interface IAdminService
    {
        IEnumerable<DrinkDTO> GetDrinks();
        DrinkDTO GetDrink(long id);
        void SaveDrink(DrinkDTO dto, byte[] image);
        void DeleteDrink(long id);
        IEnumerable<CoinDTO> GetCoins();
        CoinDTO GetCoin(long id);
        void UpdateCoin(CoinDTO dto);
    }
}