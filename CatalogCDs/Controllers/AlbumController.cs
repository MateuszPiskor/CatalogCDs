using CatalogCDs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CatalogCDs.Controllers
{
    public class AlbumController : Controller
    {
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            return View(GetAllAlbums());
        }

        private IEnumerable<Album> GetAllAlbums()
        {
            using(DBModel db = new DBModel())
            {
                return db.Albums.ToList<Album>();
            }
        }
        
        public ActionResult AddOrEdit(int id = 0)
        {
            Album album = new Album();
            return View(album);
        }

        [HttpPost]
        public ActionResult addoredit()
        {
            return View();
        }
    }
}