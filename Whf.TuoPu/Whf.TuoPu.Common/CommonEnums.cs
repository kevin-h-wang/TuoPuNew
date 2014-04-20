using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whf.TuoPu.Common
{
    #region 人员类型
    public enum PersonType
    {
        /// <summary>
        /// 公司用户
        /// </summary>
        CommonUser = 0,

        /// <summary>
        /// 供应商（史丹利等）
        /// </summary>
        Vendor = 1,

        /// <summary>
        /// 客户（买我们东西的人）
        /// </summary>
        Customer = 2,

        /// <summary>
        /// 公司经理
        /// </summary>
        Manager = 3,

        /// <summary>
        /// 系统管理员
        /// </summary>
        SysAdmin = 4,
    }
    #endregion

    #region 性别
    /// <summary>
    /// 性别
    /// </summary>
    public enum PersonSex
    {
        /// <summary>
        /// 女
        /// </summary>
        Female = 0,

        /// <summary>
        /// 男
        /// </summary>
        Male = 1,
    }
    #endregion

    #region 状态
    /// <summary>
    /// 状态
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1,

        /// <summary>
        /// 禁用
        /// </summary>
        Disable = -1
    }
    #endregion
}