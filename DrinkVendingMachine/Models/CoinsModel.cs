using DrinkVendingMachine.DTO;
using System.Collections.Generic;

namespace DrinkVendingMachine.Models
{
    public class CoinsModel
    {
        public CoinsModel(IEnumerable<CoinDTO> coins)
        {
            Coins = coins;
        }
               
        public IEnumerable<CoinDTO> Coins { get; private set; }
                
    }
}