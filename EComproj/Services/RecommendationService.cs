using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EComproj.Models;

namespace EComproj.Services
{
    public class RecommendationService
    {
        public List<Product> GetRecommendedProducts(ApplicationDbContext db, string userId, int count = 8)
        {
            var products = db.Products.Where(p => !p.IsDeleted).ToList();
            var clickCounts = db.ProductClicks
                .GroupBy(pc => pc.ProductId)
                .ToDictionary(g => g.Key, g => g.Count());

            var interestIds = db.UserInterests.Where(ui => ui.UserId == userId).Select(ui => ui.CategoryId).ToList();

            return RecommendationEngine.ComputeRecommendations(products, clickCounts, interestIds, count);
        }
    }
}