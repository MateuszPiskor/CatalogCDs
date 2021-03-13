using CatalogCDs.Models;
using System.Collections.Generic;

namespace CatalogCDs.Data
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAllAlbums();
        void AddAlbum(Album album);
        Album GetAlbum(int id);
        bool Update(Album album);
        bool DeleteAlbum(int id);
    }
}