using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Whf.TuoPu.Controller;
using Whf.TuoPu.Common;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Web
{
    public partial class _Default : LoginBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.OnInit(e);
            }
        }

        protected void ImageLogin_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                this.lblErrorInfo.Text = "用户名不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(this.txtUserPwd.Text.Trim()))
            {
                this.lblErrorInfo.Text = "用户密码不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(this.txtValidCode.Text.Trim()))
            {
                this.lblErrorInfo.Text = "验证码不能为空！";
                return;
            }
            PersonController pControl = new PersonController();
            string userName = this.txtUserName.Text.Trim();
            string passWord = WhfEncryption.DESEnCrypt(this.txtUserPwd.Text.Trim());
            PersonEntity pe = pControl.GetPersonInfo(userName, passWord);
            if (pe==null)
            {
                this.lblErrorInfo.Text = "用户名或密码不正确，请重新输入！";
                return;
            }
            if (Session["ValidationCode"] != null && Session["ValidationCode"].ToString() != this.txtValidCode.Text.Trim())
            {
                this.lblErrorInfo.Text = "验证码不正确，请重新输入！";
                return;
            }
            base.PersonEntity = pe;
            Response.Redirect("Portal/index.html");
        }
    }
}
