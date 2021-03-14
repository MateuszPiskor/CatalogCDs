using CatalogCDs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                throw new ArgumentNullException("Album dosn`t exist");
            }
            db.Albums.Add(album);
            db.SaveChanges();
        }

        public void EditAlbum(Album album)
        {
            db.Entry(album).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Album GetAlbumById(int id)
        {
            return db.Albums.FirstOrDefault(a => a.AlbumID == id);
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            return db.Albums.ToList();
        }

        public void RemoveAlbum(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("Album dosn`t exist");
            }
            db.Albums.Remove(album);
            db.SaveChanges();
        }
    }
}