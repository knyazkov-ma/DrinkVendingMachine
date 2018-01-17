namespace DrinkVendingMachine.Entity
{
    /// <summary>
    /// Напиток
    /// </summary>
    public class Drink: BaseEntity
    {
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; set; }

        public int Ord { get; set; }

    }
}