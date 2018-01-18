using DrinkVendingMachine.DTO;
using System.Collections.Generic;

namespace DrinkVendingMachine.Models
{
    public class AdminModel
    {
        public AdminModel(
            IEnumerable<DrinkDTO> drinks, 
            IEnumerable<CoinDTO> coins)
        {
            Drinks = drinks;
            Coins = coins;
        }

        public IEnumerable<DrinkDTO> Drinks { get; private set; }
        public IEnumerable<CoinDTO> Coins { get; private set; }
                
    }
}