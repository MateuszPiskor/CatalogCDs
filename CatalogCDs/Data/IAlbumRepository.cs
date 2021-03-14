using CatalogCDs.Models;
using System.Collections.Generic;

namespace CatalogCDs.Data
{
    public interface IAlbumRepository
    {
        void AddAlbum(Album album);
        IEnumerable<Album> GetAllAlbums();
        Album GetAlbumById(int id);
        void EditAlbum(Album alb);
        void RemoveAlbum(Album album);
    }
}