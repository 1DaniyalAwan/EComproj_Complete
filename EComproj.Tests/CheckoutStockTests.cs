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
    public class CheckoutStockTests
    {
        [TestMethod]
        public void ValidateStock_ReturnsNull_WhenAllGood()
        {
            var cart = new List<CartItem>
            {
                new CartItem { ProductId=1, Name="Phone", UnitPrice=100m, Quantity=1 }
            };
            var products = new List<Product>
            {
                new Product { Id=1, Name="Phone", Stock=5, ApprovalStatus=ApprovalStatus.Approved, IsDeleted=false }
            };

            var err = CheckoutEngine.ValidateStock(cart, products);
            Assert.IsNull(err);
        }

        [TestMethod]
        public void ValidateStock_ReturnsError_WhenInsufficient()
        {
            var cart = new List<CartItem>
            {
                new CartItem { ProductId=1, Name="Phone", UnitPrice=100m, Quantity=10 }
            };
            var products = new List<Product>
            {
                new Product { Id=1, Name="Phone", Stock=5, ApprovalStatus=ApprovalStatus.Approved, IsDeleted=false }
            };

            var err = CheckoutEngine.ValidateStock(cart, products);
            Assert.IsNotNull(err);
            StringAssert.Contains(err, "Insufficient stock");
        }

        [TestMethod]
        public void ValidateStock_ReturnsError_WhenUnavailable()
        {
            var cart = new List<CartItem>
            {
                new CartItem { ProductId=1, Name="Phone", UnitPrice=100m, Quantity=1 }
            };
            var products = new List<Product>
            {
                new Product { Id=1, Name="Phone", Stock=5, ApprovalStatus=ApprovalStatus.Rejected, IsDeleted=false }
            };

            var err = CheckoutEngine.ValidateStock(cart, products);
            Assert.IsNotNull(err);
            StringAssert.Contains(err, "Product unavailable");
        }
    }
}
