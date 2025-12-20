using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using System.Data.Entity; // for Include

namespace EComproj.Admin
{
    public partial class ProductApprovals : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated || !Context.User.IsInRole("Admin"))
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindPending();
            }
        }

        private void BindPending()
        {
            using (var db = new ApplicationDbContext())
            {
                var data = db.Products
                    .Where(p => p.ApprovalStatus == ApprovalStatus.Pending && !p.IsDeleted)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Stock,
                        CategoryName = p.Category.Name,
                        ImagePath = p.Images.Select(i => i.ImagePath).FirstOrDefault()
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();

                gvPending.DataSource = data;
                gvPending.DataBind();
            }
        }

        protected void gvPending_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // For ButtonField, CommandArgument is the row index
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            var id = (int)gvPending.DataKeys[rowIndex].Value;

            using (var db = new ApplicationDbContext())
            {
                var product = db.Products
                                .Include(p => p.Images)
                                .FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    lblMessage.Text = "Product not found.";
                    return;
                }

                if (e.CommandName == "Approve")
                {
                    product.ApprovalStatus = ApprovalStatus.Approved;
                    product.UpdatedAt = DateTime.UtcNow;
                    db.SaveChanges();
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Product approved.";
                }
                else if (e.CommandName == "Reject")
                {
                    // Hard delete
                    foreach (var img in product.Images.ToList())
                    {
                        var path = Server.MapPath(img.ImagePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        db.ProductImages.Remove(img);
                    }

                    db.Products.Remove(product);
                    db.SaveChanges();
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Product rejected and deleted.";
                }
            }

            BindPending();
        }
    }
}