using CatalogCDs.Models;
using System.Collections.Generic;

namespace CatalogCDs.Data
{
    public interface IAlbumRepository
    {
        void AddAlbum(Album album);
    }
}