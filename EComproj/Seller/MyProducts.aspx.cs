using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

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

        protected void gvProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProduct")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int id = (int)gvProducts.DataKeys[rowIndex].Value;

                using (var db = new ApplicationDbContext())
                {
                    var uid = Context.User.Identity.GetUserId();
                    var product = db.Products
                                    .Include("Images") // use string overload to avoid lambda Include issue
                                    .FirstOrDefault(p => p.Id == id && p.SellerId == uid && !p.IsDeleted);
                    if (product == null)
                    {
                        lblMessage.Text = "Product not found.";
                        return;
                    }

                    if (product.ApprovalStatus == ApprovalStatus.Approved)
                    {
                        lblMessage.Text = "Approved products cannot be deleted.";
                        return;
                    }

                    // Delete image files and records
                    foreach (var img in product.Images.ToList())
                    {
                        var path = Server.MapPath(img.ImagePath);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        db.ProductImages.Remove(img);
                    }

                    db.Products.Remove(product);
                    db.SaveChanges();

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Product deleted.";
                }

                BindGrid();
            }
        }
    }
}