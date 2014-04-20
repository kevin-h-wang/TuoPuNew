using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Whf.TuoPu.Controller;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Web.BasicData
{
    public partial class PersonManage : BasePage
    {

        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid(1);
                BindDropdownList();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnDelete.Attributes.Add("onclick", string.Format("return ConfirmDelete('{0}','{1}','{2}');",gvPerson.ClientID, CommonMessage.SelectOneRecord, CommonMessage.ConfirmDelete));
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.Navigator.Paging += new Web.Controls.Navigator.PagingEventHandler(Navigator_Paging);
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.gvPerson.RowDataBound += new GridViewRowEventHandler(gvPerson_RowDataBound);
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);
            base.OnInit(e);
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
            if (new PersonController().DeletePersons(lstIDS))
            {
                ShowMessage(CommonMessage.DeleteSuccess);
                BindGrid(1);
            }
            else
            {
                ShowMessage(CommonMessage.DeleteFailed);
            }
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
                Literal ltStatus = (Literal)e.Row.FindControl("ltlStatus");
                ltStatus.Text = CommonMessage.GetCommonStatus(ltStatus.Text.Trim());
                Literal ltSex = (Literal)e.Row.FindControl("ltlSex");
                ltSex.Text = CommonMessage.GetPersonSex(ltSex.Text.Trim());
                Literal ltType = (Literal)e.Row.FindControl("ltlType");
                ltType.Text = CommonMessage.GetPersonType(ltType.Text.Trim());
                btDatail.Attributes.Add("onclick", "EditPerson('" + hdf.Value + "'); return false;");
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
        #endregion

        #region 方法
        private void BindDropdownList()
        {
            drpPersonType.Items.Clear();
            drpPersonType.Items.Add(new ListItem(CommonMessage.CommonUser, PersonType.CommonUser.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.Vendor, PersonType.Vendor.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.Customer, PersonType.Customer.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.Manager, PersonType.Manager.GetHashCode().ToString()));
            drpPersonType.Items.Add(new ListItem(CommonMessage.SysAdmin, PersonType.SysAdmin.GetHashCode().ToString()));
            drpPersonType.Items.Insert(0, "");
        }

        private void BindGrid(int pageIndex)
        {
            PersonController controller = new PersonController();
            int rowCount = 0;
            DataSet dst = controller.QueryPersons(pageIndex, 10, out rowCount,this.txtEmpNO.Text.Trim(),this.txtEmpName.Text.Trim(),drpPersonType.SelectedValue);
            this.Navigator.TotalCount = rowCount;
            this.gvPerson.DataSource = dst.Tables[0];
            this.gvPerson.DataBind();
        }
        #endregion
    }
}