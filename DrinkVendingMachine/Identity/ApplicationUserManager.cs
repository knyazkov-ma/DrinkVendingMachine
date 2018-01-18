using DrinkVendingMachine.DataService.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Unity.ServiceLocation;

namespace DrinkVendingMachine.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, long>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, long> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            UserStore userStore = new UserStore(new UnityServiceLocator(UnityConfig.Container).GetInstance<IAdminUserService>());
            var manager = new ApplicationUserManager(userStore);

            return manager;
        }
        
    }
}