using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EComproj.Models;

namespace EComproj.Services
{
    public static class CheckoutEngine
    {
        // Returns null if OK; otherwise an error message
        public static string ValidateStock(List<CartItem> cart, List<Product> products)
        {
            var idx = products.ToDictionary(p => p.Id);

            foreach (var item in cart)
            {
                if (!idx.ContainsKey(item.ProductId))
                    return $"Product unavailable: {item.ProductId}";
                var p = idx[item.ProductId];
                if (p.IsDeleted || p.ApprovalStatus != ApprovalStatus.Approved)
                    return $"Product unavailable: {p.Name}";
                if (p.Stock < item.Quantity)
                    return $"Insufficient stock for {p.Name}. Available: {p.Stock}";
            }
            return null;
        }
    }
}