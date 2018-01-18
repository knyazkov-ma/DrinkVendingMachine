using DrinkVendingMachine.Entity;
using System.Data.Entity.ModelConfiguration;


namespace DrinkVendingMachine.DAL.Mapping
{
    public class AdminUserMapping : EntityTypeConfiguration<AdminUser>
    {
        public AdminUserMapping()
        {
            ToTable("AdminUser");
        }
    }
}
