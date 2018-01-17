using DrinkVendingMachine.DAL;
using DrinkVendingMachine.DataService;
using DrinkVendingMachine.Migration;
using System;
using System.Configuration;
using Unity;
using Unity.AspNet.Mvc;

namespace DrinkVendingMachine
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DrinkVendingMachine"].ConnectionString;
                        
            DbContextInstaller.Install(container, new PerRequestLifetimeManager(), connectionString);
            MigratorInstaller.Install(container, connectionString);
            DataServiceInstaller.Install(container);
            
        }
    }
}