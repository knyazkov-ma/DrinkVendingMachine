namespace DrinkVendingMachine.DTO
{
    public class CoinDTO
    {
        public long Id { get; set; }
                
        public int Denomination { get; set; }
                
        public bool Lock { get; set; }
               
        public int Count { get; set; }
    }
}