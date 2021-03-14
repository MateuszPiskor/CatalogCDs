using AutoMapper;
using CatalogCDs.Data;
using CatalogCDs.DTOs;
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
        private readonly IMapper mapper;

        public AlbumController(IAlbumRepository albumRepository, IMapper mapper)
        {
            this.albumRepository = albumRepository;
            this.mapper = mapper;
        }
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            IEnumerable<AlbumDto> albums = mapper.Map<IEnumerable<AlbumDto>>(GetAllAlbums());
            return View(albums);
        }

        private IEnumerable<Album> GetAllAlbums()
        {
            using (DBModel db = new DBModel())
            {
                return  db.Albums.ToList();
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
            return View(mapper.Map<AlbumDto>(album));
        }

        [HttpPost]
        public ActionResult AddOrEdit(AlbumDto album)
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
                        db.Albums.Add(mapper.Map<Album>(album));
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(mapper.Map<Album>(album)).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", mapper.Map<IEnumerable<AlbumDto>>(GetAllAlbums())), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", mapper.Map<IEnumerable<AlbumDto>>(GetAllAlbums())), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}