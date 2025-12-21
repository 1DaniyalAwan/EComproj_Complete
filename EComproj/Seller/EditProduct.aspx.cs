using EComproj.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComproj.Seller
{
    public partial class EditProduct : Page
    {
        private int productId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated || !Context.User.IsInRole("Seller"))
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!int.TryParse(Request.QueryString["id"], out productId))
            {
                lblMessage.Text = "Invalid product ID.";
                return;
            }

            if (!IsPostBack)
            {
                using (var db = new ApplicationDbContext())
                {
                    var uid = Context.User.Identity.GetUserId();
                    var product = db.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == productId && p.SellerId == uid && !p.IsDeleted);
                    if (product == null)
                    {
                        lblMessage.Text = "Product not found or you do not have permission.";
                        return;
                    }

                    pnlForm.Visible = true;

                    ddlCategory.DataSource = db.Categories.OrderBy(c => c.Name).ToList();
                    ddlCategory.DataTextField = "Name";
                    ddlCategory.DataValueField = "Id";
                    ddlCategory.DataBind();

                    txtName.Text = product.Name;
                    txtDescription.Text = product.Description;
                    txtPrice.Text = product.Price.ToString("0.##");
                    txtStock.Text = product.Stock.ToString();
                    ddlCategory.SelectedValue = product.CategoryId.ToString();
                    lblStatus.Text = product.ApprovalStatus.ToString();
                    imgCurrent.ImageUrl = product.Images.Select(i => i.ImagePath).FirstOrDefault() ?? "";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal price;
            int stock;
            int categoryId;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblMessage.Text = "Name is required.";
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                lblMessage.Text = "Invalid price.";
                return;
            }
            if (!int.TryParse(txtStock.Text, out stock) || stock < 0)
            {
                lblMessage.Text = "Invalid stock.";
                return;
            }
            if (!int.TryParse(ddlCategory.SelectedValue, out categoryId))
            {
                lblMessage.Text = "Invalid category.";
                return;
            }

            using (var db = new ApplicationDbContext())
            {
                var uid = Context.User.Identity.GetUserId();
                var product = db.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == productId && p.SellerId == uid && !p.IsDeleted);
                if (product == null)
                {
                    lblMessage.Text = "Product not found or you do not have permission.";
                    return;
                }

                // Detect changes
                bool baseChanged =
                    (product.Name ?? "") != txtName.Text.Trim() ||
                    (product.Description ?? "") != txtDescription.Text.Trim() ||
                    product.Price != price ||
                    product.CategoryId != categoryId;

                product.Name = txtName.Text.Trim();
                product.Description = txtDescription.Text.Trim();
                product.Price = price;
                product.Stock = stock;
                product.CategoryId = categoryId;
                product.UpdatedAt = DateTime.UtcNow;

                // If core fields changed, require re-approval
                if (baseChanged)
                {
                    product.ApprovalStatus = ApprovalStatus.Pending;
                    lblStatus.Text = "Pending";
                }

                // Replace image if uploaded
                if (fuImage.HasFile)
                {
                    var ext = Path.GetExtension(fuImage.FileName)?.ToLowerInvariant();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        // Delete previous images (and files)
                        foreach (var img in product.Images.ToList())
                        {
                            var path = Server.MapPath(img.ImagePath);
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);
                            db.ProductImages.Remove(img);
                        }

                        var fileName = $"prod_{product.Id}_{Guid.NewGuid():N}{ext}";
                        var relPath = $"~/Uploads/{fileName}";
                        fuImage.SaveAs(Server.MapPath(relPath));

                        db.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImagePath = relPath
                        });

                        imgCurrent.ImageUrl = relPath;
                    }
                    else
                    {
                        lblMessage.Text = "Only .jpg/.jpeg/.png images allowed.";
                        return;
                    }
                }

                db.SaveChanges();

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Saved.";
            }
        }
    }
}