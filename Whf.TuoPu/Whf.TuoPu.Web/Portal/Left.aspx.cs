using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Whf.TuoPu.Controller;

namespace Whf.TuoPu.Web.Portal
{
    public partial class Left : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.tvMenu.ExpandImageUrl = @"..\images\icons\TreeViewClose.gif";
                //this.tvMenu.CollapseImageUrl = @"..\images\icons\Open.gif";
                this.BindTree();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            //this.tvMenu.TreeNodeCollapsed += new TreeNodeEventHandler(tvMenu_TreeNodeCollapsed);
            //this.tvMenu.TreeNodeExpanded += new TreeNodeEventHandler(tvMenu_TreeNodeExpanded);
            base.OnInit(e);
        }

        //private void tvMenu_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        //{
        //    if (e.Node != null)
        //    {
        //        e.Node.ImageUrl = @"..\images\icons\Open.gif";
        //    }
        //}

        //private void tvMenu_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        //{
        //    if (e.Node != null)
        //    {
        //        e.Node.ImageUrl = @"..\images\icons\TreeViewClose.gif";
        //    }
        //}

        private void BindTree()
        {
            FunctionController controller = new FunctionController();
            DataSet dstMenu = controller.GetAllFunctions();
            if (dstMenu == null || dstMenu.Tables[0].Rows.Count <= 0)
            {
                TreeNode node = new TreeNode();
                node.Text = "系统功能";
                node.Value = "";
                node.NavigateUrl = "";
                this.tvMenu.Nodes.Add(node);
            }
            else
            {
                DataRow[] drs = dstMenu.Tables[0].Select("functionparentid='0'");
                if (drs.Length > 0)
                {
                    foreach (DataRow dr in drs)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = Convert.ToString(dr["functionname"]);
                        node.Value = Convert.ToString(dr["oid"]);
                        node.NavigateUrl = Convert.ToString(dr["functionurl"]);
                        this.BindChildNode(dstMenu, node);
                        this.tvMenu.Nodes.Add(node);
                    }
                }
            }
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
                        node.NavigateUrl = Convert.ToString(dr["functionurl"]);
                        node.Target = "main";
                        parNode.ChildNodes.Add(node);
                        this.BindChildNode(dstMenu, node);
                    }
                }
            }
        }
    }
}