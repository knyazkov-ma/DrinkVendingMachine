using Unity;
using Unity.Lifetime;
using Unity.Injection;

namespace DrinkVendingMachine.DAL
{
    public static class DbContextInstaller
    {
        public static void Install(IUnityContainer container, LifetimeManager lifetimeManager, string connectionString)
        {
            container.RegisterType<DbContext>(lifetimeManager, new InjectionConstructor(connectionString));
        }
    }
}
