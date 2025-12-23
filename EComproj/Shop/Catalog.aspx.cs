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
        private const int PageSize = 12;
        private int _page;

        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(Request.QueryString["page"], out _page);
            if (_page < 1) _page = 1;

            if (!IsPostBack)
            {
                using (var db = new ApplicationDbContext())
                {
                    ddlCategory.Items.Clear();
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
            _page = 1; // reset to first page on new search/filter
            BindProducts();
            BindRecommendations();
        }

        private void BindProducts()
        {
            using (var db = new ApplicationDbContext())
            {
                var query = db.Products.Where(p => p.ApprovalStatus == ApprovalStatus.Approved && !p.IsDeleted);

                // Search text
                var search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
                }

                // Category filter
                int categoryId;
                if (int.TryParse(ddlCategory.SelectedValue, out categoryId) && categoryId > 0)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                // Price filters
                decimal minPrice;
                if (decimal.TryParse(txtMinPrice.Text, out minPrice) && minPrice >= 0)
                {
                    query = query.Where(p => p.Price >= minPrice);
                }
                decimal maxPrice;
                if (decimal.TryParse(txtMaxPrice.Text, out maxPrice) && maxPrice >= 0)
                {
                    query = query.Where(p => p.Price <= maxPrice);
                }

                // Sorting
                switch (ddlSort.SelectedValue)
                {
                    case "price_asc":
                        query = query.OrderBy(p => p.Price).ThenBy(p => p.Name);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(p => p.Price).ThenBy(p => p.Name);
                        break;
                    case "newest":
                        query = query.OrderByDescending(p => p.CreatedAt).ThenBy(p => p.Name);
                        break;
                    case "name":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "popular":
                    default:
                        // Sort by click popularity, then Name
                        query = query.OrderByDescending(p => p.ProductClicks.Count).ThenBy(p => p.Name);
                        break;
                }

                var total = query.Count();
                var totalPages = (int)Math.Ceiling(total / (double)PageSize);
                if (totalPages == 0) totalPages = 1;
                if (_page > totalPages) _page = totalPages;

                var data = query
                    .Skip((_page - 1) * PageSize)
                    .Take(PageSize)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        ImagePath = p.Images.Select(i => i.ImagePath).FirstOrDefault()
                    })
                    .ToList();

                rptProducts.DataSource = data;
                rptProducts.DataBind();

                lblPageInfo.Text = $"Page {_page} of {totalPages} (Total items: {total})";

                // Prev/Next links
                lnkPrev.Visible = _page > 1;
                lnkNext.Visible = _page < totalPages;

                if (lnkPrev.Visible)
                    lnkPrev.NavigateUrl = ResolveUrl($"~/Shop/Catalog.aspx?page={_page - 1}");
                else
                    lnkPrev.NavigateUrl = string.Empty;

                if (lnkNext.Visible)
                    lnkNext.NavigateUrl = ResolveUrl($"~/Shop/Catalog.aspx?page={_page + 1}");
                else
                    lnkNext.NavigateUrl = string.Empty;
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
                var recs = service.GetRecommendedProducts(db, uid, count: 5)
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