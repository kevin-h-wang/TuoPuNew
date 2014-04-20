using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.Common
{
    public class AppCenter
    {
        public AppCenter()
        {
        }

        /// <summary>
        /// 获取Session中的PersonOID
        /// </summary>
        public static string CurrentPersonOID
        {
            get
            {
                return HttpContext.Current.Session["PersonOID"].ToString();
            }
        }

        /// <summary>
        /// 获取Session中的PersonCode
        /// </summary>
        public static string CurrentPersonAccount
        {
            get
            {
                return HttpContext.Current.Session["PersonAccount"].ToString();
            }
        }

        /// <summary>
        /// 获取Session中的PersonName
        /// </summary>
        public static string CurrentPersonName
        {
            get
            {
                return HttpContext.Current.Session["PersonName"].ToString();
            }
        }

        /// <summary>
        /// 获取Session中的PersonType
        /// </summary>
        public static PersonType CurrentPersonType
        {
            get
            {
                return (PersonType)Convert.ToInt32(HttpContext.Current.Session["PersonType"].ToString());
            }
        }

        /// <summary>
        /// 获取Session中的PersonType
        /// </summary>
        public static CommonStatus CurrentPersonStatus
        {
            get
            {
                return (CommonStatus)Convert.ToInt32(HttpContext.Current.Session["PersonStatus"].ToString());
            }
        }
    }
}
