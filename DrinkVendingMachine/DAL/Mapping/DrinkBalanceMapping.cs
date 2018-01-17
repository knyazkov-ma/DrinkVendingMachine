using DrinkVendingMachine.Entity;
using System.Data.Entity.ModelConfiguration;


namespace DrinkVendingMachine.DAL.Mapping
{
    public class DrinkBalanceMapping : EntityTypeConfiguration<DrinkBalance>
    {
        public DrinkBalanceMapping()
        {
            ToTable("DrinkBalance");
        }
    }
}
