using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;

namespace DrinkVendingMachine.Identity
{
    public class UserStore : IUserStore<ApplicationUser, long>
    {
        private readonly IAdminUserService userService;
        public UserStore(IAdminUserService userService)
        {
            this.userService = userService;
        }

        #region NotImplemented
        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        #endregion NotImplemented

        public void Dispose()
        { }

        public Task<ApplicationUser> FindByIdAsync(long userId)
        {
            AdminUserDTO u = userService.GetById(userId);

            if (u != null)
            {
            
                Task<ApplicationUser> t=  Task.Run(() => new ApplicationUser()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password
                });

            }

            return Task.Run(() => (ApplicationUser)null);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            AdminUserDTO u = userService.GetByName(userName);

            if (u != null)
            {
                return Task.Run(() => new ApplicationUser()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password
                });
            }

            return Task.Run(() => (ApplicationUser)null);
        }

        


    }


}