using DrinkVendingMachine.Entity;
using System.Data.Entity.ModelConfiguration;


namespace DrinkVendingMachine.DAL.Mapping
{
    public class SelectedDrinkMapping : EntityTypeConfiguration<SelectedDrink>
    {
        public SelectedDrinkMapping()
        {
            ToTable("SelectedDrink");
        }
    }
}
