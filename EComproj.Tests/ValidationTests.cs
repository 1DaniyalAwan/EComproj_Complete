using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EComproj.Models;

namespace EComproj.Tests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void Product_Must_Have_Name_And_Price()
        {
            var p = new Product
            {
                Name = "", // invalid
                Description = "desc",
                Price = -1m, // invalid by Range
                Stock = 10,
                CategoryId = 1,
                SellerId = "user1"
            };

            var ctx = new ValidationContext(p, null, null);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(p, ctx, results, true);

            Assert.IsFalse(valid);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void Product_Validates_Correctly()
        {
            var p = new Product
            {
                Name = "Good Product",
                Description = "desc",
                Price = 99.99m,
                Stock = 5,
                CategoryId = 1,
                SellerId = "user1"
            };

            var ctx = new ValidationContext(p, null, null);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(p, ctx, results, true);

            Assert.IsTrue(valid);
        }
    }
}
