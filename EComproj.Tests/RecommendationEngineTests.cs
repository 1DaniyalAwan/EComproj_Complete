using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EComproj.Models;
using EComproj.Services;

namespace EComproj.Tests
{
    [TestClass]
    public class RecommendationEngineTests
    {
        [TestMethod]
        public void Recommendations_Prioritize_Interest_And_Clicks()
        {
            var products = new List<Product>
            {
                new Product { Id=1, Name="Phone", CategoryId=1, ApprovalStatus=ApprovalStatus.Approved, IsDeleted=false },
                new Product { Id=2, Name="Shirt", CategoryId=2, ApprovalStatus=ApprovalStatus.Approved, IsDeleted=false },
                new Product { Id=3, Name="Laptop", CategoryId=1, ApprovalStatus=ApprovalStatus.Approved, IsDeleted=false },
                new Product { Id=4, Name="Book", CategoryId=3, ApprovalStatus=ApprovalStatus.Approved, IsDeleted=false }
            };
            var clicks = new Dictionary<int, int> { { 1, 5 }, { 2, 20 }, { 3, 10 }, { 4, 0 } };
            var interests = new List<int> { 1 }; // Electronics

            var recs = RecommendationEngine.ComputeRecommendations(products, clicks, interests, count: 3);

            // Expect electronics ranked by clicks: Laptop(10) > Phone(5), then top-up with Shirt(20)
            Assert.AreEqual(3, recs.Count);
            Assert.AreEqual(3, recs[0].Id); // Laptop
            Assert.AreEqual(1, recs[1].Id); // Phone
            Assert.AreEqual(2, recs[2].Id); // Shirt
        }
    }
}
