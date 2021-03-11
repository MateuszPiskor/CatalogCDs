//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CatalogCDs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Album
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public Nullable<int> Year { get; set; }
        public string Artist { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public Album()
        {
            ImagePath = "~/AppFiles/Images/empty.jpg";
        }
    }
}
