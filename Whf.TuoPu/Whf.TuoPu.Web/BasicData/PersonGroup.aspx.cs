using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Whf.TuoPu.Controller;
using System.Data;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Web.BasicData
{
    public partial class PersonGroup : BasePage
    {
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid(1);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnDelete.Attributes.Add("onclick", string.Format("return ConfirmDelete('{0}','{1}','{2}');", gvPerson.ClientID, CommonMessage.SelectOneRecord, CommonMessage.ConfirmDelete));
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.Navigator.Paging += new Web.Controls.Navigator.PagingEventHandler(Navigator_Paging);
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.gvPerson.RowDataBound += new GridViewRowEventHandler(gvPerson_RowDataBound);
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);
            base.OnInit(e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.BindGrid(1);
        }

        private void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Button btDatail = ((Button)e.Row.FindControl("btnEdit"));
                HiddenField hdf = (HiddenField)e.Row.FindControl("hdfOID");
                btDatail.Attributes.Add("onclick", "EditGroup('" + hdf.Value + "');return false;");

                Button btnSelect = ((Button)e.Row.FindControl("btnSelect"));
                btnSelect.Attributes.Add("onclick", "SelectPerson('" + hdf.Value + "')");

                Button btnPermission = ((Button)e.Row.FindControl("btnPermission"));
                btnPermission.Attributes.Add("onclick", "SelectPermission('" + hdf.Value + "')");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid(1);
        }

        private void Navigator_Paging(object sender, Controls.Navigator.PagingEventArgs e)
        {
            this.BindGrid(e.NewPage);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> lstIDS = new List<string>();
            foreach (GridViewRow row in gvPerson.Rows)
            {
                var cbx = (CheckBox)row.FindControl("ckb");
                if (cbx.Checked == true)
                {
                    var hdf = (HiddenField)row.FindControl("hdfOID");
                    lstIDS.Add(hdf.Value);
                }
            }
            if (new PermissionGroupController().DeletePermissionGroup(lstIDS))
            {
                ShowMessage(CommonMessage.DeleteSuccess);
                BindGrid(1);
            }
            else
            {
                ShowMessage(CommonMessage.DeleteFailed);
            }
        }
        #endregion

        #region 方法
        private void BindGrid(int pageIndex)
        {
            PermissionGroupController controller = new PermissionGroupController();
            int rowCount = 0;
            DataSet dst = controller.QueryGroupPermissions(pageIndex, 10, out rowCount, this.txtGroupCode.Text.Trim(), this.txtGroupName.Text.Trim());
            this.Navigator.TotalCount = rowCount;
            this.gvPerson.DataSource = dst.Tables[0];
            this.gvPerson.DataBind();
        }
        #endregion
    }
}