using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EComproj.Models;

namespace EComproj.Services
{
    public class RecommendationService
    {
        // Get recommended approved products for a user:
        // 1) If user has interests, pick approved products in those categories
        // 2) Order by popularity (clicks)
        // 3) Fallback: global trending by clicks
        public List<Product> GetRecommendedProducts(ApplicationDbContext db, string userId, int count = 8)
        {
            var approved = db.Products.Where(p => !p.IsDeleted && p.ApprovalStatus == ApprovalStatus.Approved);

            var userInterests = db.UserInterests.Where(ui => ui.UserId == userId).Select(ui => ui.CategoryId).ToList();
            IQueryable<Product> baseQuery;

            if (userInterests.Any())
            {
                baseQuery = approved.Where(p => userInterests.Contains(p.CategoryId));
            }
            else
            {
                baseQuery = approved;
            }

            var result = baseQuery
                .Select(p => new
                {
                    Product = p,
                    Clicks = p.ProductClicks.Count
                })
                .OrderByDescending(x => x.Clicks)
                .ThenBy(x => x.Product.Name)
                .Take(count)
                .Select(x => x.Product)
                .ToList();

            // If interests produced too few, top up with global trending
            if (result.Count < count)
            {
                var topUp = approved
                    .Where(p => !result.Select(r => r.Id).Contains(p.Id))
                    .Select(p => new { Product = p, Clicks = p.ProductClicks.Count })
                    .OrderByDescending(x => x.Clicks)
                    .ThenBy(x => x.Product.Name)
                    .Take(count - result.Count)
                    .Select(x => x.Product)
                    .ToList();

                result.AddRange(topUp);
            }

            return result;
        }
    }
}