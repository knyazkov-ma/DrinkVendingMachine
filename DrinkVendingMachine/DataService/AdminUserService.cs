using DrinkVendingMachine.DAL;
using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;
using System.Linq;

namespace DrinkVendingMachine.DataService
{
    public class AdminUserService : IAdminUserService
    {
        private readonly DbContext dbContext;
        public AdminUserService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
                
        public AdminUserDTO GetById(long id)
        {
            return dbContext.AdminUsers.Select(u => new AdminUserDTO {Id = u.Id, UserName = u.UserName, Password = u.Password }).FirstOrDefault(u => u.Id == id);
        }

        public AdminUserDTO GetByName(string name)
        {
            return dbContext.AdminUsers.Select(u => new AdminUserDTO { Id = u.Id, UserName = u.UserName, Password = u.Password }).FirstOrDefault(u => u.UserName.ToUpper() == name.ToUpper());
        }
    }
}