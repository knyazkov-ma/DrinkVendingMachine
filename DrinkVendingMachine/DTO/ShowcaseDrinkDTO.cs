namespace DrinkVendingMachine.DTO
{
    public class ShowcaseDrinkDTO
    {
        public long Id { get; set; }
        public decimal Cost { get; set; }
        public int Count { get; set; }
        public bool Selected { get; set; }

        public bool CanbeSelected { get; set; }
    }
}