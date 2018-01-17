using DrinkVendingMachine.Entity;
using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System;
using System.Data.Entity.ModelConfiguration;

namespace DrinkVendingMachine.DAL
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext(string connectionString) : base(connectionString)
        {

        }

        public DbSet<Coin> Coins { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkBalance> DrinkBalances { get; set; }
        public DbSet<PaymentCoin> PaymentCoins { get; set; }
        public DbSet<SelectedDrink> SelectedDrinks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(type => !String.IsNullOrEmpty(type.Namespace))
                 .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                     && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}