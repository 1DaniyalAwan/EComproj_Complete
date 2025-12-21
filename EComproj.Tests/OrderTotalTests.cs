using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using EComproj.Models;
using EComproj.Services;

namespace EComproj.Tests
{
    [TestClass]
    public class OrderTotalTests
    {
        [TestMethod]
        public void Cart_Total_Computes_Correctly()
        {
            var cart = new List<CartItem>
            {
                new CartItem { ProductId=1, Name="A", UnitPrice=10.00m, Quantity=2 },
                new CartItem { ProductId=2, Name="B", UnitPrice=5.50m, Quantity=3 }
            };

            var total = CartHelper.ComputeTotal(cart);
            Assert.AreEqual(10.00m * 2 + 5.50m * 3, total);
        }
    }
}
