using AutoMapper;
using CatalogCDs.Data;
using CatalogCDs.DTOs;
using CatalogCDs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace CatalogCDs.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository albumRepository;
        private readonly IMapper mapper;

        public AlbumController(IMapper mapper, IAlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Entry point to the app
        /// </summary>
        /// <returns>Initial View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets list of albums
        /// </summary>
        /// <returns>View with all albums</returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            IEnumerable<AlbumDto> albums = mapper.Map<IEnumerable<AlbumDto>>(albumRepository.GetAllAlbums());
            return View(albums);
        }

        /// <summary>
        /// Get album by id, or create empty
        /// </summary>
        /// <returns>View with album details</returns>
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            Album album = new Album();
            if (id != 0)
            {
                album = albumRepository.GetAlbumById(id);
            }
            return View(mapper.Map<AlbumDto>(album));
        }

        /// <summary>
        /// Add new album to db, or edit it when exist  
        /// </summary>
        /// <returns>Json with infromation about operation and html code </returns>
        [HttpPost]
        public ActionResult AddOrEdit(AlbumDto album)
        {
            try
            {
                if (album.ImageUpload != null)
                {
                    GenerateImagePath(album);
                }

                if (album.AlbumID == 0)
                {
                    albumRepository.AddAlbum(mapper.Map<Album>(album));
                }
                else
                {
                    albumRepository.EditAlbum(mapper.Map<Album>(album));
                }

                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", mapper.Map<IEnumerable<AlbumDto>>(albumRepository.GetAllAlbums())), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Delete album from db
        /// </summary>
        /// <returns>Json with infromation about operation and html code</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Album album = albumRepository.GetAlbumById(id);
                albumRepository.RemoveAlbum(album);
                return Json(new { success = true, html = RazorToString.RenderRazorView(this, "GetAll", mapper.Map<IEnumerable<AlbumDto>>(albumRepository.GetAllAlbums())), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void GenerateImagePath(AlbumDto album)
        {
            string fileName = Path.GetFileNameWithoutExtension(album.ImageUpload.FileName);
            string extension = Path.GetExtension(album.ImageUpload.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            album.ImagePath = "~/AppFiles/Images/" + fileName;
            string path = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
            album.ImageUpload.SaveAs(path);
        }

    }
}