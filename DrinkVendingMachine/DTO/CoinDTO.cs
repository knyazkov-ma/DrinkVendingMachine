namespace DrinkVendingMachine.DTO
{
    public class CoinDTO
    {
        public long Id { get; set; }
        
        /// <summary>
        /// Номинал
        /// </summary>
        public int Denomination { get; set; }

        
        /// <summary>
        /// Блокировка (монета не принимается)
        /// </summary>
        public bool Lock { get; set; }
    }
}