namespace DrinkVendingMachine.Entity
{
    /// <summary>
    /// Оплата монетой
    /// </summary>
    public class PaymentCoin: BaseEntity
    {
        public Coin Coin { get; set; }
        public int Count { get; set; }
    }
}