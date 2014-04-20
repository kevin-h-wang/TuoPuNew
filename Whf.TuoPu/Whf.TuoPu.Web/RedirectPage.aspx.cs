using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Whf.TuoPu.Web
{
    public partial class RedirectPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script = "window.parent.location='Default.aspx'";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", script, true);
        }
    }
}