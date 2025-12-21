using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EComproj.Models;

namespace EComproj.Services
{
    public static class RecommendationEngine
    {
        // Pure method for unit testing
        public static List<Product> ComputeRecommendations(
            List<Product> products,
            Dictionary<int, int> clickCounts,
            List<int> interestCategoryIds,
            int count)
        {
            var approved = products.Where(p => !p.IsDeleted && p.ApprovalStatus == ApprovalStatus.Approved);

            IEnumerable<Product> baseQuery = approved;
            if (interestCategoryIds != null && interestCategoryIds.Any())
            {
                baseQuery = baseQuery.Where(p => interestCategoryIds.Contains(p.CategoryId));
            }

            var ranked = baseQuery
                .Select(p => new
                {
                    Product = p,
                    Clicks = clickCounts != null && clickCounts.ContainsKey(p.Id) ? clickCounts[p.Id] : 0
                })
                .OrderByDescending(x => x.Clicks)
                .ThenBy(x => x.Product.Name)
                .Take(count)
                .Select(x => x.Product)
                .ToList();

            if (ranked.Count < count)
            {
                var extra = approved
                    .Where(p => !ranked.Select(r => r.Id).Contains(p.Id))
                    .Select(p => new
                    {
                        Product = p,
                        Clicks = clickCounts != null && clickCounts.ContainsKey(p.Id) ? clickCounts[p.Id] : 0
                    })
                    .OrderByDescending(x => x.Clicks)
                    .ThenBy(x => x.Product.Name)
                    .Take(count - ranked.Count)
                    .Select(x => x.Product)
                    .ToList();

                ranked.AddRange(extra);
            }

            return ranked;
        }
    }
}