using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EComproj.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Store relative path, e.g., ~/Uploads/abc.jpg
        [Required, StringLength(500)]
        public string ImagePath { get; set; }
    }
}