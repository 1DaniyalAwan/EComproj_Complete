using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EComproj.Models
{
    public class ProductClick
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public DateTime ClickedAt { get; set; } = DateTime.UtcNow;
    }
}