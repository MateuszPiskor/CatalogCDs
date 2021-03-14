using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CatalogCDs.DTOs
{
    public class AlbumDto
    {
        public int AlbumID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Genre { get; set; }
        public Nullable<int> Year { get; set; }
        public string Artist { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public AlbumDto()
        {
            ImagePath = "~/AppFiles/Images/empty.jpg";
        }
    }
}