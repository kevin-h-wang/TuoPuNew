using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Web.Portal
{
    public partial class Top : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
        }

        public void InitPage()
        {
            this.lblCurrentUser.Text = AppCenter.CurrentPersonName;
            this.lblDateTime.Text = DateTime.Now.ToLongDateString().ToString();
        }
    }
}