using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using EComproj.Services;
using Microsoft.AspNet.Identity;

namespace EComproj
{
    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated || !Context.User.IsInRole("Customer"))
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            var cart = CartHelper.GetCart(Session);
            if (!cart.Any())
            {
                lblMessage.Text = "Your cart is empty.";
                return;
            }

            using (var db = new ApplicationDbContext())
            {
                // Validate stock
                var productIds = cart.Select(x => x.ProductId).ToList();
                var dbProducts = db.Products.Where(p => productIds.Contains(p.Id)).ToList();

                foreach (var item in cart)
                {
                    var p = dbProducts.FirstOrDefault(x => x.Id == item.ProductId);
                    if (p == null || p.IsDeleted || p.ApprovalStatus != ApprovalStatus.Approved)
                    {
                        lblMessage.Text = "Product unavailable: " + (p?.Name ?? item.ProductId.ToString());
                        return;
                    }
                    if (p.Stock < item.Quantity)
                    {
                        lblMessage.Text = $"Insufficient stock for {p.Name}. Available: {p.Stock}";
                        return;
                    }
                }

                var order = new Order
                {
                    CustomerId = Context.User.Identity.GetUserId(),
                    CreatedAt = DateTime.UtcNow,
                    Status = OrderStatus.Confirmed,
                    TotalAmount = CartHelper.ComputeTotal(cart)
                };
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cart)
                {
                    db.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });

                    var p = db.Products.First(x => x.Id == item.ProductId);
                    p.Stock -= item.Quantity; // reduce stock
                }

                db.SaveChanges();

                // Clear cart
                cart.Clear();
                CartHelper.SaveCart(Session, cart);

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = $"Order #{order.Id} placed successfully.";
            }
        }
    }
}