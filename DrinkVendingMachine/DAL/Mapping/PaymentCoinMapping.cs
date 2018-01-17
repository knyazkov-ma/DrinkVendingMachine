using DrinkVendingMachine.Entity;
using System.Data.Entity.ModelConfiguration;


namespace DrinkVendingMachine.DAL.Mapping
{
    public class PaymentCoinMapping : EntityTypeConfiguration<PaymentCoin>
    {
        public PaymentCoinMapping()
        {
            ToTable("PaymentCoin");

            HasRequired(a => a.Coin).WithOptional().Map(t => t.MapKey("CoinId"));


        }
    }
}
