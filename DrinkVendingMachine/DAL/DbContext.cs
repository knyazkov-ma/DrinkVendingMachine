using DrinkVendingMachine.Entity;
using System.Data.Entity;

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
            modelBuilder.Entity<Coin>().ToTable("Coin");
            modelBuilder.Entity<Drink>().ToTable("Drink");
            modelBuilder.Entity<DrinkBalance>().ToTable("DrinkBalance");
            modelBuilder.Entity<PaymentCoin>().ToTable("PaymentCoin");
            modelBuilder.Entity<SelectedDrink>().ToTable("SelectedDrink");
        }
    }
}