namespace DrinkVendingMachine.Entity
{
    /// <summary>
    /// Остаток напитка
    /// </summary>
    public class DrinkBalance: BaseEntity
    {
        public long DrinkId { get; set; }
        public int Count { get; set; }
    }
}