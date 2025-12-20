using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComproj.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Range(0, 999999999)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        // Seller (AspNetUsers.Id)
        [Required]
        public string SellerId { get; set; }
        public virtual ApplicationUser Seller { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Soft-delete flag (optional)
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductClick> ProductClicks { get; set; }
    }
}