using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Whf.TuoPu.Controller;
using System.Data;
using Whf.TuoPu.Common;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Web.BasicData
{
    public partial class FunctionManage : BasePage
    {
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindTree();
                this.BindDropdownList();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnDelete.Attributes.Add("onclick", string.Format("return ConfirmDelete('{0}');",CommonMessage.ConfirmDelete));
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.tvMenu.SelectedNodeChanged += new EventHandler(tvMenu_SelectedNodeChanged);
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            base.OnInit(e);
        }

        private void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvMenu.SelectedNode;
            if (selectedNode.Value == "0")
            {
                btnSave.Visible = false;
                hfParentOID.Value = selectedNode.Value;
            }
            else
            {
                FunctionEntity fun = new FunctionController().GetFunc(selectedNode.Value);
                if (fun != null)
                {
                    hfParentOID.Value = selectedNode.Value;
                    hdfOID.Value = selectedNode.Value;
                    txtFuncCode.Text = fun.FUNCTIONKEY;
                    txtFuncMemo.Text = fun.MEMO;
                    txtFuncName.Text = fun.FUNCTIONNAME;
                    txtFuncOrder.Text = fun.FUNCTIONORDER.ToString();
                    txtFuncUrl.Text = fun.FUNCTIONURL;
                    txtFuncLevel.Text = fun.FUNCTIONLEVEL.ToString();
                    drpFuncStatus.SelectedValue = fun.FUNCTIONSTATUS.ToString();
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindTree();
        }
        #endregion

        #region 操作
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvMenu.SelectedNode;
            if (selectedNode==null)
            {
                base.ShowMessage("请选择上一级菜单！");
                return;
            }
            else if (selectedNode.Value != null)
            {
                this.btnSave.Visible = true;
                this.ClearAllControls();
                FunctionEntity fun = new FunctionController().GetFunc(selectedNode.Value);
                if (fun == null)
                {
                    txtFuncLevel.Text = "0";
                }
                else
                {
                    txtFuncLevel.Text = (Convert.ToInt32(txtFuncLevel.Text) + 1).ToString();
                }
                txtFuncOrder.Text = new FunctionController().GetChildMaxOrder(selectedNode.Value);
            }
        }

        private void ClearAllControls()
        {
            this.hdfOID.Value = "";
            this.txtFuncCode.Text = "";
            this.txtFuncMemo.Text = "";
            this.txtFuncName.Text = "";
            this.txtFuncOrder.Text = "";
            this.txtFuncUrl.Text = "";
        }

        /// <summary>
        /// 验证保存
        /// </summary>
        /// <returns></returns>
        private bool CheckSave()
        {
            bool returnValue = true;
            string errorMsg = string.Empty;
            if (string.IsNullOrEmpty(txtFuncCode.Text.Trim()))
            {
                errorMsg += "菜单编码不能为空！";
            }
            else if (txtFuncCode.Text.Trim().Length > 40)
            {
                errorMsg +="菜单编码长度不能超过40！";
            }
            if (string.IsNullOrEmpty(txtFuncName.Text.Trim()))
            {
                errorMsg += "菜单名称不能为空！";
            }
            if (txtFuncName.Text.Length>40)
            {
                errorMsg += "菜单名称长度不能超过40！";
            }
            if (!string.IsNullOrEmpty(this.txtFuncMemo.Text.Trim()) && this.txtFuncMemo.Text.Length>100)
            {
                errorMsg += "菜单说明长度不能超过100！";
            }
            if (!string.IsNullOrEmpty(this.txtFuncUrl.Text.Trim()) && this.txtFuncUrl.Text.Length > 100)
            {
                errorMsg += "菜单地址长度不能超过100！";
            }
            if (!base.CheckInteger(txtFuncOrder.Text.Trim()))
            {
                errorMsg += "菜单顺序必须是数字！";
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                returnValue = false;
                base.ShowMessage(errorMsg);
            }
            return returnValue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckSave())
            {
                bool retValue = true;
                FunctionController controler = new FunctionController();
                FunctionEntity fun = new FunctionEntity();
                fun.OID = hdfOID.Value;
                fun.FUNCTIONPARENTID = hfParentOID.Value;
                fun.FUNCTIONKEY = txtFuncCode.Text.Trim();
                fun.FUNCTIONLEVEL = Convert.ToInt32(txtFuncLevel.Text.Trim());
                fun.FUNCTIONNAME = txtFuncName.Text.Trim();
                fun.FUNCTIONORDER = Convert.ToInt32(txtFuncOrder.Text.Trim());
                fun.FUNCTIONSTATUS = drpFuncStatus.SelectedValue;
                fun.FUNCTIONTYPE = 0;
                fun.FUNCTIONURL = txtFuncUrl.Text.Trim();
                fun.MEMO = txtFuncMemo.Text.Trim();
                fun.CUSER = AppCenter.CurrentPersonAccount;
                fun.MUSER = AppCenter.CurrentPersonAccount;
                if (string.IsNullOrEmpty(fun.OID))
                {
                    fun.OID = Guid.NewGuid().ToString();
                    retValue = controler.InsertFunction(fun);
                }
                else
                {
                    retValue = controler.UpdateFunction(fun);
                }
                if (retValue)
                {
                    base.ShowMessage(CommonMessage.SaveSuccess);
                    this.ClearAllControls();
                    this.BindTree();
                }
                else
                {
                    base.ShowMessage(CommonMessage.SaveFailed);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvMenu.SelectedNode;
            if (selectedNode == null)
            {
                base.ShowMessage("请选择要删除的菜单！");
                return;
            }
            else if (selectedNode.Value != null)
            {
                if (selectedNode.Value == "0")
                {
                    base.ShowMessage("根节点不能删除！");
                    return;
                }
                List<string> lstOIDs = new List<string>();
                lstOIDs.Add(selectedNode.Value);
                if (new FunctionController().DeleteFunction(lstOIDs))
                {
                    base.ShowMessage(CommonMessage.DeleteSuccess);
                    this.ClearAllControls();
                    this.BindTree();
                }
                else
                {
                    base.ShowMessage(CommonMessage.DeleteFailed);
                }
            }
            
        }
        #endregion

        #region 方法
        private void BindDropdownList()
        {
            this.drpFuncStatus.Items.Clear();
            ListItem lst1 = new ListItem();
            lst1.Text = CommonMessage.EnableStatus;
            lst1.Value = CommonStatus.Enable.GetHashCode().ToString();
            this.drpFuncStatus.Items.Add(lst1);

            ListItem lst2 = new ListItem();
            lst2.Text = CommonMessage.DisableStatus;
            lst2.Value = CommonStatus.Disable.GetHashCode().ToString();
            this.drpFuncStatus.Items.Add(lst2);
        }

        private void BindTree()
        {
            this.tvMenu.Nodes.Clear();
            FunctionController controller = new FunctionController();
            DataSet dstMenu = controller.QueryFunctions(this.txtQueryFunCode.Text.Trim(), this.txtQueryFunName.Text.Trim());
            TreeNode node = new TreeNode();
            node.Text = "系统功能";
            node.Value = "0";
            node.NavigateUrl = "";
            this.tvMenu.Nodes.Add(node);
            if (dstMenu != null && dstMenu.Tables[0].Rows.Count > 0)
            {
                this.BindChildNode(dstMenu, node);
            }
            this.tvMenu.ExpandAll();
        }

        private void BindChildNode(DataSet dstMenu, TreeNode parNode)
        {
            if (parNode != null && dstMenu != null)
            {
                string parID = parNode.Value;
                DataRow[] drs = dstMenu.Tables[0].Select(string.Format("functionparentid='{0}'", parID));
                if (drs.Length > 0)
                {
                    foreach (DataRow dr in drs)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = Convert.ToString(dr["functionname"]);
                        node.Value = Convert.ToString(dr["oid"]);
                        node.NavigateUrl = "";
                        parNode.ChildNodes.Add(node);
                        this.BindChildNode(dstMenu, node);
                    }
                }
            }
        }
        #endregion
    }
}