using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using Microsoft.AspNet.Identity;

namespace EComproj.Seller
{
    public partial class MyProducts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated || !Context.User.IsInRole("Seller"))
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            using (var db = new ApplicationDbContext())
            {
                var uid = Context.User.Identity.GetUserId();
                var data = db.Products
                    .Where(p => p.SellerId == uid && !p.IsDeleted)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Stock,
                        CategoryName = p.Category.Name,
                        ApprovalStatus = p.ApprovalStatus.ToString(),
                        ImagePath = p.Images.Select(i => i.ImagePath).FirstOrDefault()
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();

                gvProducts.DataSource = data;
                gvProducts.DataBind();
            }
        }
    }
}