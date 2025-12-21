using EComproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EComproj.Services
{
    public static class CartHelper
    {
        private const string Key = "CART_ITEMS";

        public static List<CartItem> GetCart(HttpSessionState session)
        {
            var cart = session[Key] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                session[Key] = cart;
            }
            return cart;
        }

        public static void SaveCart(HttpSessionState session, List<CartItem> cart)
        {
            session[Key] = cart;
        }

        public static void AddOrUpdate(List<CartItem> cart, CartItem item)
        {
            var existing = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing == null)
            {
                cart.Add(item);
            }
            else
            {
                existing.Quantity += item.Quantity;
            }
        }

        public static decimal ComputeTotal(List<CartItem> cart)
        {
            return cart.Sum(x => x.UnitPrice * x.Quantity);
        }
    }
}