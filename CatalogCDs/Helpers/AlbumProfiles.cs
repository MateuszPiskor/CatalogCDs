using AutoMapper;
using CatalogCDs.DTOs;
using CatalogCDs.Models;

namespace CatalogCDs.Helpers
{
    public class AlbumProfiles: Profile
    {
        public AlbumProfiles()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<AlbumDto, Album>();
        }
    }
}