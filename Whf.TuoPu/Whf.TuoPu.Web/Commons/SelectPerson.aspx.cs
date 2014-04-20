using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Whf.TuoPu.Controller;
using System.Data;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Web.Commons
{
    public partial class SelectPerson : BasePage
    {
        #region 属性
        /// <summary>
        /// 业务ID
        /// </summary>
        private string BusinessID
        {
            get 
            {
                if (ViewState["BusinessID"] == null)
                {
                    return "";
                }
                else
                {
                    return ViewState["BusinessID"] as string;
                }
            }
            set
            {
                ViewState["BusinessID"] = value;
            }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        private string BusinessModel
        {
            get
            {
                if (ViewState["BusinessModel"] == null)
                {
                    return "";
                }
                else
                {
                    return ViewState["BusinessModel"] as string;
                }
            }
            set
            {
                ViewState["BusinessModel"] = value;
            }
        }
        #endregion

        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BusinessID = Request.QueryString["BusinessID"] ?? "";
                this.BusinessModel = Request.QueryString["BusinessModel"] ?? "";
                this.BindGrid(1);
                BindDropdownList();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.Navigator.Paging += new Web.Controls.Navigator.PagingEventHandler(Navigator_Paging);
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.gvPerson.RowDataBound += new GridViewRowEventHandler(gvPerson_RowDataBound);
            this.btnConfirm.Click += new EventHandler(btnConfirm_Click);
            base.OnInit(e);
        }

        private void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                HiddenField hdf = (HiddenField)e.Row.FindControl("hdfOID");
                Literal ltStatus = (Literal)e.Row.FindControl("ltlStatus");
                ltStatus.Text = CommonMessage.GetCommonStatus(ltStatus.Text.Trim());
                Literal ltSex = (Literal)e.Row.FindControl("ltlSex");
                ltSex.Text = CommonMessage.GetPersonSex(ltSex.Text.Trim());
                Literal ltType = (Literal)e.Row.FindControl("ltlType");
                ltType.Text = CommonMessage.GetPersonType(ltType.Text.Trim());
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

        #region 操作
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool returnValue = true;
            List<string> selectedPersons = this.GetSelectedPersons();
            if (selectedPersons.Count > 0)
            {
                switch (this.BusinessModel.ToUpper())
                { 
                    case "PERSONGROUP":
                        returnValue=new GroupPersonMapController().SaveGroupPerson(selectedPersons,this.BusinessID,AppCenter.CurrentPersonAccount);
                        break;
                    default:
                        break;
                }
            }
            if (returnValue)
            {
                this.hdfFlag.Value = "1";
                base.ShowMessage(CommonMessage.SaveSuccess);
            }
            else
            {
                base.ShowMessage(CommonMessage.SaveFailed); 
            }
        }
        #endregion

        #region 方法
        private void BindGrid(int pageIndex)
        {
            PersonController controller = new PersonController();
            int rowCount = 0;
            DataSet dst = dst = controller.QueryPersons(pageIndex, 10, out rowCount, this.txtEmpNO.Text.Trim(), this.txtEmpName.Text.Trim(),drpPersonType.SelectedValue);
            this.Navigator.TotalCount = rowCount;
            this.gvPerson.DataSource = dst.Tables[0];
            this.gvPerson.DataBind();
        }

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

        private List<string> GetSelectedPersons()
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
            return lstIDS;
        }
        #endregion
    }
}