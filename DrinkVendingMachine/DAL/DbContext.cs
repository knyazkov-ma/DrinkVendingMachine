namespace DrinkVendingMachine.DAL
{
    public class DbContext: System.Data.Entity.DbContext
    {
        public DbContext(string connectionString): base(connectionString)
        { }
    }
}