using Anca.Rizan.WebRentC.Core.Contracts;
using Anca.Rizan.WebRentC.Core.Models;
using AncaRizan.WebRentC.DataAccess.SQL;
using System;

using Unity;

namespace AncaRizan.WebRentC.WebUI
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
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

           container.RegisterType<IRepository<Reservation>, SQLRepository<Reservation>>();
           container.RegisterType<IRepository<Car>, SQLRepository<Car>>();
           container.RegisterType<IRepository<Customer>, SQLRepository<Customer>>();
           container.RegisterType<IRepository<Location>, SQLRepository<Location>>();

        }
    }
}