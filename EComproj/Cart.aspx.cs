using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using EComproj.Services;

namespace EComproj
{
    public partial class Cart : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            var cart = CartHelper.GetCart(Session);
            gvCart.DataSource = cart.Select(x => new { x.ProductId, x.Name, x.UnitPrice, x.Quantity }).ToList();
            gvCart.DataBind();
            lblTotal.Text = string.Format("{0:C}", CartHelper.ComputeTotal(cart));
        }

        protected void gvCart_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                var productId = (int)gvCart.DataKeys[rowIndex].Value;
                var cart = CartHelper.GetCart(Session);
                var item = cart.FirstOrDefault(x => x.ProductId == productId);
                if (item != null) cart.Remove(item);
                CartHelper.SaveCart(Session, cart);
                BindCart();
            }
        }
    }
}