using Unity;
using Unity.Injection;

namespace DrinkVendingMachine.Migration
{
    public class MigratorInstaller
    {
        public static void Install(IUnityContainer container, string connectionString)
        {

            container.RegisterType<IMigrationRunner, MigrationRunner>(
                new InjectionConstructor(connectionString));
        }
    }
}
