using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Whf.TuoPu.Web.Controls
{
    public partial class Navigator : System.Web.UI.UserControl
    {
        #region 记录总数
        public int TotalCount
        {
            get
            {
                if (ViewState["TotalCount"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(ViewState["TotalCount"]);
                }
            }
            set
            {
                ViewState["TotalCount"] = value;
            }
        }
        #endregion

        private readonly int CountPerPage = 10;

        #region 当前页数
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(ViewState["CurrentPage"]);
                }
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        #endregion

        #region 总页数
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(ViewState["PageCount"]);
                }
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        #endregion

        #region 定义委托和分页事件
        public delegate void PagingEventHandler(Object sender, PagingEventArgs e);
        public event PagingEventHandler Paging; //声明事件

        // 定义PagingEventArgs类，传递给Observer所感兴趣的信息
        public class PagingEventArgs : EventArgs
        {
            public readonly int NewPage;
            public PagingEventArgs(int CurrentPage)
            {
                this.NewPage = CurrentPage;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitControls();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.lbtnFirstPage.Click += new EventHandler(lbtnFirstPage_Click);
            this.lbtnPrePage.Click += new EventHandler(lbtnPrePage_Click);
            this.lbtnNextPage.Click += new EventHandler(lbtnNextPage_Click);
            this.lbtnLastPage.Click += new EventHandler(lbtnLastPage_Click);
            this.btnGO.Click += new EventHandler(btnGO_Click);
            this.drpPageIndex.SelectedIndexChanged += new EventHandler(drpPageIndex_SelectedIndexChanged);
            base.OnPreRender(e);
        }

        #region 初始化分页控件
        private void InitControls()
        {
            //如果记录总数为0，不显示分页控件
            if (TotalCount == 0)
            {
                divPaging.Visible = false;
            }
            else
            {
                this.CurrentPage = 1;
                this.PageCount = (int)Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalCount) / Convert.ToDouble(CountPerPage)));//获取总页数
                SetTotalCountLabelValue(this.TotalCount);
                SetCurrentPageLabelValue(this.CurrentPage);
                SetPageCountLabelValue(this.PageCount);
                for (int i = 1; i <= this.PageCount; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString();
                    item.Value = i.ToString();
                    this.drpPageIndex.Items.Add(item);
                }
                this.EnableDisableControls();
            }
        }
        #endregion

        #region 首页
        private void lbtnFirstPage_Click(object sender, EventArgs e)
        {
            this.CurrentPage = 1;
            this.OnPaging(this.CurrentPage);
            this.EnableDisableControls();
        }
        #endregion

        #region 上一页
        private void lbtnPrePage_Click(object sender, EventArgs e)
        {
            if (this.CurrentPage > 1)
            {
                this.CurrentPage--;
                this.OnPaging(this.CurrentPage);
                this.EnableDisableControls();
            }
        }
        #endregion

        #region 下一页
        private void lbtnNextPage_Click(object sender, EventArgs e)
        {
            if (this.CurrentPage < this.PageCount)
            {
                this.CurrentPage++;
                this.OnPaging(this.CurrentPage);
                this.EnableDisableControls();
            }
        }
        #endregion

        #region 尾页
        private void lbtnLastPage_Click(object sender, EventArgs e)
        {
            this.CurrentPage = this.PageCount;
            this.OnPaging(this.CurrentPage);
            this.EnableDisableControls();
        }
        #endregion

        #region 跳转
        private void btnGO_Click(object sender, EventArgs e)
        {
            string page = this.txtPage.Text.Trim();
            int cpage = 1;
            if (int.TryParse(page, out cpage))
            {
                if (cpage > 0 && cpage <= PageCount)
                {
                    this.CurrentPage = cpage;
                    this.OnPaging(this.CurrentPage);
                    this.EnableDisableControls();
                }
            }
        }
        #endregion

        #region 下拉框变化
        private void drpPageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CurrentPage = Convert.ToInt32(drpPageIndex.SelectedValue);
            this.OnPaging(this.CurrentPage);
            this.EnableDisableControls();
        }
        #endregion

        #region 分页操作
        protected void OnPaging(int CurrentPage)
        {
            this.SetCurrentPageLabelValue(CurrentPage);
            this.drpPageIndex.SelectedValue = CurrentPage.ToString();
            PagingEventArgs e = new PagingEventArgs(CurrentPage);
            if (Paging != null)
            {
                Paging(this, e);
            }
        }
        #endregion

        #region 给Label赋值
        private void SetTotalCountLabelValue(int totalCount)
        {
            this.lblTotalCount.Text = string.Format("共{0}条", totalCount.ToString());
        }
        private void SetCurrentPageLabelValue(int currentPage)
        {
            this.lblCurrentPage.Text = string.Format("第{0}页", currentPage.ToString());
        }
        private void SetPageCountLabelValue(int PageCount)
        {
            this.lblTotalPage.Text = string.Format("共{0}页", PageCount.ToString());
        }
        #endregion

        #region 启用禁用翻页按钮
        private void EnableDisableControls()
        {
            if (this.PageCount == 1)
            {
                this.lbtnFirstPage.Enabled = false;
                this.lbtnNextPage.Enabled = false;
                this.lbtnPrePage.Enabled = false;
                this.lbtnLastPage.Enabled = false;
            }
            else
            {
                this.lbtnFirstPage.Enabled = true;
                this.lbtnPrePage.Enabled = true;
                this.lbtnNextPage.Enabled = true;
                this.lbtnLastPage.Enabled = true;
                if (this.CurrentPage == 1)
                {
                    this.lbtnFirstPage.Enabled = false;
                    this.lbtnPrePage.Enabled = false;
                }
                else if (this.CurrentPage == this.PageCount)
                {
                    this.lbtnNextPage.Enabled = false;
                    this.lbtnLastPage.Enabled = false;
                }
            }
        }
        #endregion
    }
}