using EComproj.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComproj.Seller
{
    public partial class AddProduct : Page
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
                using (var db = new ApplicationDbContext())
                {
                    ddlCategory.DataSource = db.Categories.OrderBy(c => c.Name).ToList();
                    ddlCategory.DataTextField = "Name";
                    ddlCategory.DataValueField = "Id";
                    ddlCategory.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblMessage.Text = "Name is required.";
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                lblMessage.Text = "Invalid price.";
                return;
            }

            int stock;
            if (!int.TryParse(txtStock.Text, out stock) || stock < 0)
            {
                lblMessage.Text = "Invalid stock.";
                return;
            }

            int categoryId;
            if (!int.TryParse(ddlCategory.SelectedValue, out categoryId))
            {
                lblMessage.Text = "Category is required.";
                return;
            }

            using (var db = new ApplicationDbContext())
            {
                var product = new Product
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Price = price,
                    Stock = stock,
                    CategoryId = categoryId,
                    SellerId = Context.User.Identity.GetUserId(),
                    ApprovalStatus = ApprovalStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                db.Products.Add(product);
                db.SaveChanges(); // get Product.Id

                // Save image if provided
                if (fuImage.HasFile)
                {
                    var ext = Path.GetExtension(fuImage.FileName)?.ToLowerInvariant();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        var fileName = $"prod_{product.Id}_{Guid.NewGuid():N}{ext}";
                        var relPath = $"~/Uploads/{fileName}";
                        var absPath = Server.MapPath(relPath);
                        fuImage.SaveAs(absPath);

                        db.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImagePath = relPath
                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        lblMessage.Text = "Only .jpg/.jpeg/.png images allowed.";
                        return;
                    }
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Product submitted for approval.";
                txtName.Text = "";
                txtDescription.Text = "";
                txtPrice.Text = "";
                txtStock.Text = "";
                ddlCategory.ClearSelection();
            }
        }
    }
}