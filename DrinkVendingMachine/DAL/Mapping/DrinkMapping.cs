using DrinkVendingMachine.Entity;
using System.Data.Entity.ModelConfiguration;


namespace DrinkVendingMachine.DAL.Mapping
{
    public class DrinkMapping : EntityTypeConfiguration<Drink>
    {
        public DrinkMapping()
        {
            ToTable("Drink");
        }
    }
}
