using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComproj.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Range(0, 999999999)]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}