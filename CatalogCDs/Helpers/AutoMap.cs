using AutoMapper;

namespace CatalogCDs.Helpers
{
    public static class AutoMap
    {
        public static IMapper Mapper { get; set; }

        public static void RegisterMappings()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
               config =>
               {
                   config.AddProfile<AlbumProfiles>();
               });

            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}