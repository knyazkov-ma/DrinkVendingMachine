using DrinkVendingMachine.Entity;
using System.Data.Entity.ModelConfiguration;


namespace DrinkVendingMachine.DAL.Mapping
{
    public class CoinMapping : EntityTypeConfiguration<Coin>
    {
        public CoinMapping()
        {
            ToTable("Coin");
        }
    }
}
