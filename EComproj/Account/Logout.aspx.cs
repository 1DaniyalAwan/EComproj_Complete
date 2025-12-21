using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace EComproj.Account
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = Context.GetOwinContext().Authentication;
            auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Response.Redirect("~/");
        }
    }
}