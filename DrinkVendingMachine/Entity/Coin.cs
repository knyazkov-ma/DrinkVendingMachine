namespace DrinkVendingMachine.Entity
{
    /// <summary>
    /// Номиналы монеты
    /// </summary>
    public enum CoinDenomination
    {
        One = 1,
        Two = 2,
        Five = 5,
        Ten = 10
    }

    /// <summary>
    /// Монета
    /// </summary>
    public class Coin: BaseEntity
    {
        /// <summary>
        /// Номинал
        /// </summary>
        public CoinDenomination Denomination { get; set; }

        /// <summary>
        /// Количество монет, которые присутствуют в автомате для сдачи
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Блокировка (монета не принимается)
        /// </summary>
        public bool Lock { get; set; }
    }
}