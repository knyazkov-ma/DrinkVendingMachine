using DrinkVendingMachine.Aspects;
using DrinkVendingMachine.DAL;
using DrinkVendingMachine.DataService.Interface;

namespace DrinkVendingMachine.DataService
{
    [Transaction]
    public class AdminService : IAdminService
    {
        private readonly DbContext dbContext;
        public AdminService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }                
        
    }
}