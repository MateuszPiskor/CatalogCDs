using CatalogCDs.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult AddOrEdit(Album album)
        {
            try
            {
                if (album.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(album.ImageUpload.FileName);
                    string extension = Path.GetExtension(album.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    album.ImagePath = "~/AppFiles/Images/" + fileName;
                    string path = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
                    album.ImageUpload.SaveAs(path);
                }
                using (DBModel db = new DBModel())
                {

                    db.Albums.Add(album);
                    db.SaveChanges();
                }

                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", GetAllAlbums()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}