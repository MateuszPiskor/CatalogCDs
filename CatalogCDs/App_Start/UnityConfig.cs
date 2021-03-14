using CatalogCDs.Data;
using CatalogCDs.Helpers;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CatalogCDs
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IAlbumRepository, AlbumRepository>();
            container.RegisterInstance(AutoMap.Mapper);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}