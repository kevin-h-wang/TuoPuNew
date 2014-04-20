using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whf.TuoPu.Common
{
    public static class CommonMessage
    {
        #region EnumText
        public const string EnableStatus = "启用";
        public const string DisableStatus = "禁用";

        public const string Female = "女";
        public const string Male = "男";

        public const string CommonUser = "公司用户";
        public const string Vendor = "供应商";
        public const string Customer = "客户";
        public const string Manager = "公司经理";
        public const string SysAdmin = "系统管理员";
        #endregion

        #region AlertMessage
        public const string SaveSuccess = "保存成功！";
        public const string SaveFailed = "保存失败！";
        public const string ConfirmDelete = "确定要删除吗？";
        public const string DeleteSuccess = "删除成功！";
        public const string DeleteFailed = "删除失败！";
        public const string SelectOneRecord = "请先选择一行！";
        #endregion

        #region 获取多语言
        /// <summary>
        /// 获取人员类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPersonType(string type)
        {
            PersonType personType = (PersonType)int.Parse(type);
            switch (personType)
            { 
                case PersonType.Vendor:
                    return Vendor;
                case PersonType.Customer:
                    return Customer;
                case PersonType.Manager:
                    return Manager;
                case PersonType.SysAdmin:
                    return SysAdmin;
                default:
                    return CommonUser;
            }
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static string GetPersonSex(string sex)
        {
            PersonSex personSex = (PersonSex)int.Parse(sex);
            switch (personSex)
            { 
                case PersonSex.Female:
                    return Female;
                default:
                    return Male;
            }
        }
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetCommonStatus(string status)
        {
            CommonStatus commonStatus = (CommonStatus)int.Parse(status);
            switch (commonStatus)
            { 
                case CommonStatus.Disable:
                    return DisableStatus;
                default:
                    return EnableStatus;
            }
        }
        #endregion
    }
}