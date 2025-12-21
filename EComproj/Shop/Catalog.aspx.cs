using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using EComproj.Services;
using Microsoft.AspNet.Identity;

namespace EComproj.Shop
{
    public partial class Catalog : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var db = new ApplicationDbContext())
                {
                    ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem("-- All --", "0"));
                    foreach (var c in db.Categories.OrderBy(x => x.Name).ToList())
                    {
                        ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem(c.Name, c.Id.ToString()));
                    }
                }
                BindProducts();
                BindRecommendations();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindProducts();
            BindRecommendations();
        }

        private void BindProducts()
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.Products.Where(p => p.ApprovalStatus == ApprovalStatus.Approved && !p.IsDeleted);

                var search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
                }

                int categoryId;
                if (int.TryParse(ddlCategory.SelectedValue, out categoryId) && categoryId > 0)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                var data = query
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        ImagePath = p.Images.Select(i => i.ImagePath).FirstOrDefault()
                    })
                    .OrderBy(x => x.Name)
                    .ToList();

                rptProducts.DataSource = data;
                rptProducts.DataBind();
            }
        }

        private void BindRecommendations()
        {
            if (!Context.User.Identity.IsAuthenticated || !Context.User.IsInRole("Customer"))
            {
                pnlRecommendations.Visible = false;
                return;
            }

            using (var db = new ApplicationDbContext())
            {
                var uid = Context.User.Identity.GetUserId();
                var service = new RecommendationService();
                var recs = service.GetRecommendedProducts(db, uid, count: 6)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        ImagePath = p.Images.Select(i => i.ImagePath).FirstOrDefault()
                    })
                    .ToList();

                pnlRecommendations.Visible = recs.Any();
                rptRecommendations.DataSource = recs;
                rptRecommendations.DataBind();
            }
        }
    }
}