using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EComproj.Models;
using Microsoft.AspNet.Identity;

namespace EComproj.Account
{
    public partial class MyInterests : Page
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
                using (var db = new ApplicationDbContext())
                {
                    pnlInterests.Visible = true;
                    var cats = db.Categories.OrderBy(c => c.Name).ToList();
                    cblInterests.DataSource = cats;
                    cblInterests.DataTextField = "Name";
                    cblInterests.DataValueField = "Id";
                    cblInterests.DataBind();

                    var uid = Context.User.Identity.GetUserId();
                    var selected = db.UserInterests.Where(ui => ui.UserId == uid).Select(ui => ui.CategoryId).ToList();
                    foreach (System.Web.UI.WebControls.ListItem item in cblInterests.Items)
                    {
                        item.Selected = selected.Contains(int.Parse(item.Value));
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var uid = Context.User.Identity.GetUserId();
                var existing = db.UserInterests.Where(ui => ui.UserId == uid).ToList();
                db.UserInterests.RemoveRange(existing);

                foreach (System.Web.UI.WebControls.ListItem item in cblInterests.Items)
                {
                    if (item.Selected)
                    {
                        db.UserInterests.Add(new UserInterest
                        {
                            UserId = uid,
                            CategoryId = int.Parse(item.Value)
                        });
                    }
                }

                db.SaveChanges();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Interests updated.";
            }
        }
    }
}