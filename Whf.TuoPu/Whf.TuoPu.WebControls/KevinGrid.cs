using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace Whf.TuoPu.UserControls
{
    public class KevinGrid : GridView, INamingContainer
    {
        #region 属性
        /// <summary>
        /// 是否显示用于选择的复选框的操作列
        /// </summary>
        [Bindable(true), Category("定制"), DefaultValue(false)]
        public bool ShowCheckBox { private get; set; }

        /// <summary>
        ///获取或设置选择列标题
        /// </summary>
        [Bindable(true), Category("定制"), Localizable(true)]
        public string CheckTemplateHeaderText
        {
            private get
            {
                return ViewState["CheckTemplateHeaderText"] == null
                           ? "选择"
                           : ViewState["CheckTemplateHeaderText"].ToString();
            }
            set { ViewState["CheckTemplateHeaderText"] = value; }
        }

        private string _rowClickButtonID;
        /// <summary>
        /// 单击行事件所对应的按钮的ID
        /// </summary>
        [Description("单击行事件所对应的按钮的ID"), DefaultValue(""), Category("扩展")]
        public virtual string RowClickButtonID
        {
            get { return _rowClickButtonID; }
            set { _rowClickButtonID = value; }
        }

        private string _rowDoubleClickButtonID;
        /// <summary>
        /// 双击行事件所对应的按钮的ID
        /// </summary>
        [Description("双击行事件所对应的按钮的ID"), DefaultValue(""), Category("扩展")]
        public virtual string RowDoubleClickButtonID
        {
            get { return _rowDoubleClickButtonID; }
            set { _rowDoubleClickButtonID = value; }
        }
        #endregion


        /// <summary>
        /// 初始化
        /// </summary>
        public KevinGrid()
        {
            //ShowHeaderWhenEmpty = true;
        }

        /// <summary>
        /// 初始化GridView,CheckBox列
        /// </summary>
        /// <param name="e"></param> 
        protected override void OnInit(EventArgs e)
        {
            if (base.DesignMode == false)
            {
                base.HeaderStyle.Wrap = false;

                if (ShowCheckBox)
                {
                    var template = new TemplateField();
                    template.ShowHeader = ShowCheckBox;
                    if (ShowCheckBox == false)
                    {
                        template.HeaderStyle.Width = 0;
                        template.ItemStyle.Width = 0;
                    }
                    else
                    {
                        template.HeaderText = CheckTemplateHeaderText;
                        template.HeaderStyle.CssClass = base.HeaderStyle.CssClass;
                        template.HeaderStyle.Width = 50;
                        template.ItemStyle.Width = 50;
                    }
                    template.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    template.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                    template.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    base.Columns.Insert(0, template);
                }
            }
            base.OnInit(e);
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!String.IsNullOrEmpty(RowClickButtonID) || !String.IsNullOrEmpty(RowDoubleClickButtonID))
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("jsClickAndDoubleClick"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(
                        this.GetType(),
                        "jsClickAndDoubleClick", JavaScriptConstant.jsClickAndDoubleClick
                        );
                }
            }
        }

        /// <summary>
        /// GridView生成数据行时生成CheckBox列，添加单击双击的按钮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (base.DesignMode == false && e.Row.RowType == DataControlRowType.Header && ShowCheckBox)
            {
                Literal litHeader = new Literal();
                litHeader.Text = "选择";
                e.Row.Cells[0].Controls.Add(litHeader);
            }
            if (base.DesignMode == false && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ShowCheckBox)
                {
                    var cb = new CheckBox();
                    cb.ID = "FI_ChkSelect";
                    e.Row.Cells[0].Controls.Add(cb);
                }
            }

            base.OnRowCreated(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover","currentcolor=this.style.backgroundColor;this.style.backgroundColor='778899';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");

                if (!String.IsNullOrEmpty(RowClickButtonID) || !String.IsNullOrEmpty(RowDoubleClickButtonID))
                {
                    // GridViewRow的每个TableCell
                    foreach (TableCell tc in e.Row.Cells)
                    {
                        // TableCell里的每个Control
                        foreach (Control c in tc.Controls)
                        {
                            // 如果控件继承自接口IButtonControl
                            if (c.GetType().GetInterface("IButtonControl") != null && c.GetType().GetInterface("IButtonControl").Equals(typeof(IButtonControl)))
                            {
                                if (!String.IsNullOrEmpty(RowClickButtonID))
                                {
                                    // 该按钮的ID等于单击行所对应的按钮ID
                                    if (c.ID == RowClickButtonID)
                                    {
                                        // 增加行的单击事件，调用客户端脚本，根据所对应按钮的ID执行所对应按钮的click事件
                                        e.Row.Attributes.Add("onclick", "javascript:yy_RowClick('" + c.ClientID + "')");
                                    }
                                }

                                if (!String.IsNullOrEmpty(RowDoubleClickButtonID))
                                {
                                    // 该按钮的ID等于双击行所对应的按钮ID
                                    if (c.ID == RowDoubleClickButtonID)
                                    {
                                        // 增加行的双击事件，调用客户端脚本，根据所对应按钮的ID执行所对应按钮的click事件
                                        e.Row.Attributes.Add("ondblclick", "javascript:yy_RowDoubleClick('" + c.ClientID + "')");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            base.OnRowDataBound(e);
        }
    }

    /// <summary>
    /// javascript
    /// </summary>
    public class JavaScriptConstant
    {
        internal const string jsClickAndDoubleClick = @"<script type=""text/javascript"">
        //<![CDATA[
        var isDoubleClick = false;
        function yy_RowClick(id)
        {
            setTimeout(""yy_RowClickTimeout('""+id+""')"", 300);
        }
        function yy_RowClickTimeout(id)
        {
            if (isDoubleClick == false)
            {
                // 执行ID所指按钮的click事件
                document.getElementById(id).click();
            }
            isDoubleClick = true;
        }
        function yy_RowDoubleClick(id)
        {
            if (isDoubleClick == true)
            {
                // 执行ID所指按钮的click事件
                document.getElementById(id).click();
            }
            isDoubleClick = true;
        }
        //]]>
        </script>";
    }
}
