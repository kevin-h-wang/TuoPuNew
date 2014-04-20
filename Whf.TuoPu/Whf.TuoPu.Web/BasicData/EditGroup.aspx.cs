using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Whf.TuoPu.Common;
using Whf.TuoPu.Entity;
using Whf.TuoPu.Controller;

namespace Whf.TuoPu.Web.BasicData
{
    public partial class EditGroup : BasePage
    {
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string groupID = Request.QueryString["GroupID"] ?? "";
                if (string.IsNullOrEmpty(groupID))
                {
                    lblTitleNew.Visible = true;
                    lblTitleEdit.Visible = false;
                }
                else
                {
                    lblTitleNew.Visible = false;
                    lblTitleEdit.Visible = true;
                }
                hdfGroupID.Value = groupID;
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
            PermissionGroupEntity group = new PermissionGroupEntity();
            group.OID = hdfGroupID.Value;
            group.CUSER = AppCenter.CurrentPersonAccount;
            group.MUSER = AppCenter.CurrentPersonAccount;
            group.GroupCode = txtGroupCode.Text.Trim();
            group.GroupName = txtGroupName.Text.Trim();
            group.GroupStatus = Convert.ToInt32(drpGroupStatus.SelectedValue);
            group.MEMO = txtGroupMemo.Text.Trim();
            bool retValue = true;
            if (string.IsNullOrEmpty(group.OID))
            {
                group.OID = Guid.NewGuid().ToString();
                retValue = new PermissionGroupController().InsertGroup(group);
            }
            else
            {
                retValue = new PermissionGroupController().UpdateGroup(group);
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
            if (string.IsNullOrEmpty(txtGroupCode.Text.Trim()))
            {
                errorMsg += "群组编码不能为空！";
            }
            else if (txtGroupCode.Text.Trim().Length > 40)
            {
                errorMsg += "群组编码长度不能超过40！";
            }

            if (string.IsNullOrEmpty(txtGroupName.Text.Trim()))
            {
                errorMsg += "群组名称不能为空！";
            }
            else if (txtGroupName.Text.Length > 40)
            {
                errorMsg += "群组名称长度不能超过40！";
            }
            if (!string.IsNullOrEmpty(this.txtGroupMemo.Text.Trim()) && this.txtGroupMemo.Text.Length > 40)
            {
                errorMsg += "备注信息长度不能超过100！";
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
            PermissionGroupEntity person = new PermissionGroupController().GetGroupInfo(this.hdfGroupID.Value);
            if (person != null)
            {
                txtGroupCode.Text = person.GroupCode;
                txtGroupName.Text = person.GroupName;
                drpGroupStatus.SelectedValue = person.GroupStatus.ToString();
                txtGroupMemo.Text = person.MEMO;
            }
        }

        private void BindDropdownList()
        { 
            drpGroupStatus.Items.Clear();
            drpGroupStatus.Items.Add(new ListItem(CommonMessage.EnableStatus, CommonStatus.Enable.GetHashCode().ToString()));
            drpGroupStatus.Items.Add(new ListItem(CommonMessage.DisableStatus, CommonStatus.Disable.GetHashCode().ToString()));
        }
        #endregion
    }
}