using CatalogCDs.Data;
using CatalogCDs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace CatalogCDs.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository albumRepository;

        public AlbumController(IAlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
        }
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
            using (DBModel db = new DBModel())
            {
                return db.Albums.ToList<Album>();
            }
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Album album = new Album();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    album = db.Albums.Where(x => x.AlbumID == id).FirstOrDefault<Album>();
                }
            }
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
                    if (album.AlbumID == 0)
                    {
                        db.Albums.Add(album);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(album).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", GetAllAlbums()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Album album = db.Albums.Where(x => x.AlbumID == id).FirstOrDefault<Album>();
                    db.Albums.Remove(album);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", GetAllAlbums()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}