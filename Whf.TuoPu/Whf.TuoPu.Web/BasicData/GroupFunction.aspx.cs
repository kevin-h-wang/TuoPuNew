using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Whf.TuoPu.Controller;
using Whf.TuoPu.Entity;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Web.BasicData
{
    public partial class GroupFunction : BasePage
    {
        #region 页面属性
        private string CurrentGroupID
        {
            get
            {
                if (ViewState["CurrentGroupID"] == null)
                {
                    return "";
                }
                else
                {
                    return ViewState["CurrentGroupID"] as string;
                }
            }
            set
            {
                ViewState["CurrentGroupID"] = value;
            }
        }
        private GroupFunctionMapController _controller;
        public GroupFunctionMapController Controller
        {
            get
            {
                if (_controller == null)
                {
                    _controller = new GroupFunctionMapController();
                }
                return _controller;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CurrentGroupID = Request.QueryString["GroupID"] ?? "";
                this.BindTree();
                btnBindTreeByGrouID();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            base.OnInit(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<GroupFunctionMapEntity> entitys = new List<GroupFunctionMapEntity>();
            foreach (TreeNode node in this.tvMenu.CheckedNodes)
            {
                GroupFunctionMapEntity en = new GroupFunctionMapEntity();
                en.OID = Guid.NewGuid().ToString();
                en.GroupID = this.CurrentGroupID;
                en.FunctionID = node.Value;
                en.CUSER = AppCenter.CurrentPersonAccount;
                en.MUSER = AppCenter.CurrentPersonAccount;
                entitys.Add(en);
            }
            if (Controller.SaveGroupFunction(entitys))
            {
                base.ShowMessage(CommonMessage.SaveSuccess);
            }
            else
            {
                base.ShowMessage(CommonMessage.SaveFailed);
            }
        }

        //根据GroupID绑定树
        protected void btnBindTreeByGrouID()
        {
            DataTable dtFuncs = Controller.GetFuncByGroup(this.CurrentGroupID);
            var nodes = tvMenu.CheckedNodes;
            if (dtFuncs != null && dtFuncs.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFuncs.Rows)
                {
                    foreach (TreeNode node in tvMenu.Nodes)
                    {
                        CheckNodes(node, Convert.ToString(dr["functionID"]));
                    }
                }
            }
        }

        private void CheckNodes(TreeNode node, string funcID)
        {
            if (node.Value == funcID)
            {
                node.Checked = true;
            }
            else
            {
                foreach (TreeNode cNode in node.ChildNodes)
                {
                    CheckNodes(cNode, funcID);
                }
            }
        }
        #region 方法

        private void BindTree()
        {
            this.tvMenu.Nodes.Clear();
            FunctionController controller = new FunctionController();
            DataSet dstMenu = controller.GetAllFunctions();
            TreeNode node = new TreeNode();
            node.Text = "系统功能";
            node.Value = "0";
            node.NavigateUrl = "";
            node.ShowCheckBox = true;
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
                        node.ShowCheckBox = true;
                        this.BindChildNode(dstMenu, node);
                    }
                }
            }
        }
        #endregion
    }
}