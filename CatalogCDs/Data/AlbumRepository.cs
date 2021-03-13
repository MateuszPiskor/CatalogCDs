using CatalogCDs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace CatalogCDs.Data
{
    public class AlbumRepository : IAlbumRepository
    {
        [Dependency]
        public DBModel db { get; set; }

        public void AddAlbum(Album album)
        {
            if(album == null)
            {
                throw new ArgumentNullException("album");
            }
            db.Albums.Add(album);
            db.SaveChanges();
        }
    }
}