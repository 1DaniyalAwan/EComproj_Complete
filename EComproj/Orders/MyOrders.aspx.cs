using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using Microsoft.AspNet.Identity;

namespace EComproj.Orders
{
    public partial class MyOrders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated || !Context.User.IsInRole("Customer"))
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindOrders();
                BindItems();
            }
        }

        private void BindOrders()
        {
            var uid = Context.User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var orders = db.Orders
                    .Where(o => o.CustomerId == uid)
                    .OrderByDescending(o => o.Id)
                    .Select(o => new { o.Id, o.CreatedAt, Status = o.Status.ToString(), o.TotalAmount })
                    .ToList();

                gvOrders.DataSource = orders;
                gvOrders.DataBind();
            }
        }

        private void BindItems()
        {
            var uid = Context.User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var items = db.OrderItems
                    .Where(oi => db.Orders.Any(o => o.Id == oi.OrderId && o.CustomerId == uid))
                    .Select(oi => new
                    {
                        oi.OrderId,
                        ProductName = oi.Product.Name,
                        oi.Quantity,
                        oi.UnitPrice,
                        LineTotal = oi.UnitPrice * oi.Quantity
                    })
                    .OrderByDescending(x => x.OrderId)
                    .ToList();

                gvItems.DataSource = items;
                gvItems.DataBind();
            }
        }
    }
}