using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Whf.TuoPu.Entity;
using Whf.TuoPu.Controller;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Web.BasicData
{
    public partial class Editperson : BasePage
    {
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string personID = Request.QueryString["PersonID"] ?? "";
                if (string.IsNullOrEmpty(personID))
                {
                    lblTitleNew.Visible = true;
                    lblTitleEdit.Visible = false;
                }
                else
                {
                    lblTitleNew.Visible = false;
                    lblTitleEdit.Visible = true;
                }
                hdfPersonID.Value = personID;
                this.BindDropdownList();
                this.BindControls();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            base.OnInit(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckSave())
            {
                PersonEntity per = new PersonEntity();
                per.OID = hdfPersonID.Value;
                per.CUSER = AppCenter.CurrentPersonAccount;
                per.MUSER = AppCenter.CurrentPersonAccount;
                per.PERSONACCOUNT = txtPersonAccount.Text.Trim();
                per.PERSONCODE = txtPersonAccount.Text.Trim();
                per.PERSONDESC = txtPersonMemo.Text.Trim();
                per.PERSONEMAIL = txtEmail.Text.Trim();
                per.PERSONMEMO = txtPersonMemo.Text.Trim();
                per.PERSONMOBILEPHONE = txtMobilPhone.Text.Trim();
                per.PERSONNAME = txtPersonName.Text.Trim();
                per.PERSONOFFICEPHONE = txtOfficePhone.Text.Trim();
                per.PERSONSEX = Convert.ToInt32(drpPersonSex.SelectedValue);
                per.PERSONSTATUS = Convert.ToInt32(drpPersonStatus.SelectedValue);
                per.PERSONTYPE = Convert.ToInt32(drpPersonType.SelectedValue);
                bool retValue = true;
                if (string.IsNullOrEmpty(per.OID))
                {
                    per.OID = Guid.NewGuid().ToString();
                    retValue = new PersonController().InsertPerson(per);
                }
                else
                {
                    retValue = new PersonController().UpdatePersonAdmin(per);
                }
                if (retValue)
                {
                    hdfFlag.Value = "1";
                    base.ShowMessage(CommonMessage.SaveSuccess);
                }
                else
                {
                    base.ShowMessage(CommonMessage.SaveFailed);
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 验证保存
        /// </summary>
        /// <returns></returns>
        private bool CheckSave()
        {
            bool returnValue = true;
            string errorMsg = string.Empty;
            if (string.IsNullOrEmpty(txtPersonAccount.Text.Trim()))
            {
                errorMsg += "员工账号不能为空！";
            }
            else if (txtPersonAccount.Text.Trim().Length > 40)
            {
                errorMsg += "员工账号长度不能超过40！";
            }

            if (string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                errorMsg += "员工姓名不能为空！";
            }
            else if (txtPersonName.Text.Length > 40)
            {
                errorMsg += "员工姓名长度不能超过40！";
            }
            if (string.IsNullOrEmpty(txtOfficePhone.Text.Trim()))
            {
                errorMsg += "办公电话不能为空！";
            }
            else if (txtOfficePhone.Text.Length > 40)
            {
                errorMsg += "办公电话长度不能超过40！";
            }
            if (string.IsNullOrEmpty(txtMobilPhone.Text.Trim()))
            {
                errorMsg += "移动电话不能为空！";
            }
            else if (txtMobilPhone.Text.Length > 40)
            {
                errorMsg += "移动电话长度不能超过40！";
            }
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                errorMsg += "电子邮件不能为空！";
            }
            else if (txtEmail.Text.Length > 40)
            {
                errorMsg += "电子邮件长度不能超过40！";
            }
            else if (!base.CheckEmail(txtEmail.Text.Trim()))
            {
                errorMsg += "电子邮件格式不正确！";
            }
            if (!string.IsNullOrEmpty(this.txtPersonMemo.Text.Trim()) && this.txtPersonMemo.Text.Length > 40)
            {
                errorMsg += "备注信息长度不能超过40！";
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                returnValue = false;
                base.ShowMessage(errorMsg);
            }
            return returnValue;
        }

        private void BindControls()
        {
            PersonEntity person = new PersonController().GetPersonInfo(this.hdfPersonID.Value);
            if (person != null)
            {
                txtEmail.Text = person.PERSONEMAIL;
                txtMobilPhone.Text = person.PERSONMOBILEPHONE;
                txtOfficePhone.Text = person.PERSONOFFICEPHONE;
                txtPersonAccount.Text = person.PERSONACCOUNT;
                txtPersonMemo.Text = person.PERSONMEMO;
                txtPersonName.Text = person.PERSONNAME;
                drpPersonSex.SelectedValue = person.PERSONSEX.ToString();
                drpPersonStatus.SelectedValue = person.PERSONSTATUS.ToString();
                drpPersonType.SelectedValue = person.PERSONTYPE.ToString();
            }
        }

        private void BindDropdownList()
        { 
            drpPersonSex.Items.Clear();
            drpPersonSex.Items.Add(new ListItem(CommonMessage.Male,PersonSex.Male.GetHashCode().ToString()));
            drpPersonSex.Items.Add(new ListItem(CommonMessage.Female,PersonSex.Female.GetHashCode().ToString()));

            drpPersonStatus.Items.Clear();
            drpPersonStatus.Items.Add(new ListItem(CommonMessage.EnableStatus, CommonStatus.Enable.GetHashCode().ToString()));
            drpPersonStatus.Items.Add(new ListItem(CommonMessage.DisableStatus, CommonStatus.Disable.GetHashCode().ToString()));

            drpPersonType.Items.Clear();

            drpPersonType.Items.Add(new ListItem(CommonMessage.CommonUser, PersonType.CommonUser.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.Vendor, PersonType.Vendor.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.Customer, PersonType.Customer.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.Manager, PersonType.Manager.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.SysAdmin, PersonType.SysAdmin.GetHashCode().ToString()));
        }
        #endregion
    }
}