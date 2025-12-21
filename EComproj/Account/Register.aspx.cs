using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using EComproj.Models;
using Microsoft.Owin.Security;

namespace EComproj.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind interests from Categories
                using (var db = new ApplicationDbContext())
                {
                    var cats = db.Categories.OrderBy(c => c.Name).ToList();
                    cblInterests.DataSource = cats;
                    cblInterests.DataTextField = "Name";
                    cblInterests.DataValueField = "Id";
                    cblInterests.DataBind();
                }

                // Default: Customer interests visible
                pnlInterests.Visible = (rblRole.SelectedValue == "Customer");
                rblRole.SelectedIndexChanged += rblRole_SelectedIndexChanged;
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            var email = Email.Text.Trim();
            var password = Password.Text;
            var confirm = ConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                lblMessage.Text = "Email is required.";
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "Password is required.";
                return;
            }
            if (password != confirm)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            var role = rblRole.SelectedValue;
            if (role != "Customer" && role != "Seller")
            {
                lblMessage.Text = "Please select a role.";
                return;
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var user = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = manager.Create(user, password);

            if (result.Succeeded)
            {
                // Add role
                manager.AddToRole(user.Id, role);

                // Save interests for customers
                if (role == "Customer")
                {
                    using (var db = new ApplicationDbContext())
                    {
                        foreach (var item in cblInterests.Items.Cast<System.Web.UI.WebControls.ListItem>())
                        {
                            if (item.Selected)
                            {
                                var catId = int.Parse(item.Value);
                                db.UserInterests.Add(new UserInterest
                                {
                                    UserId = user.Id,
                                    CategoryId = catId
                                });
                            }
                        }
                        db.SaveChanges();
                    }
                }

                // Auto sign-in and redirect
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("~/");
            }
            else
            {
                lblMessage.Text = string.Join("; ", result.Errors);
            }
        }

        protected void rblRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlInterests.Visible = (rblRole.SelectedValue == "Customer");
        }
    }
}