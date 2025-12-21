using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using Microsoft.AspNet.Identity;
using EComproj.Services;

namespace EComproj.Shop
{
    public partial class ProductDetails : Page
    {
        private int productId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out productId))
            {
                lblMessage.Text = "Invalid product.";
                return;
            }

            if (!IsPostBack)
            {
                using (var db = new ApplicationDbContext())
                {
                    var p = db.Products.Where(x => x.Id == productId && x.ApprovalStatus == ApprovalStatus.Approved && !x.IsDeleted)
                                       .Select(x => new
                                       {
                                           x.Id,
                                           x.Name,
                                           x.Description,
                                           x.Price,
                                           x.Stock,
                                           ImagePath = x.Images.Select(i => i.ImagePath).FirstOrDefault()
                                       })
                                       .FirstOrDefault();
                    if (p == null)
                    {
                        lblMessage.Text = "Product not found.";
                        return;
                    }

                    pnlDetails.Visible = true;
                    lblName.Text = p.Name;
                    lblDescription.Text = p.Description;
                    lblPrice.Text = string.Format("{0:C}", p.Price);
                    lblStock.Text = p.Stock.ToString();
                    imgMain.Src = p.ImagePath ?? "";
                }

                // Track click if logged in
                if (Context.User.Identity.IsAuthenticated)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        db.ProductClicks.Add(new ProductClick
                        {
                            ProductId = productId,
                            UserId = Context.User.Identity.GetUserId(),
                            ClickedAt = DateTime.UtcNow
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int qty;
            if (!int.TryParse(txtQty.Text, out qty) || qty < 1)
            {
                lblMessage.Text = "Invalid quantity.";
                return;
            }

            using (var db = new ApplicationDbContext())
            {
                var p = db.Products.FirstOrDefault(x => x.Id == productId && x.ApprovalStatus == ApprovalStatus.Approved && !x.IsDeleted);
                if (p == null)
                {
                    lblMessage.Text = "Product not found.";
                    return;
                }

                var cart = CartHelper.GetCart(Session);
                CartHelper.AddOrUpdate(cart, new CartItem
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    UnitPrice = p.Price,
                    Quantity = qty
                });
                CartHelper.SaveCart(Session, cart);

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Added to cart.";
            }
        }
    }
}