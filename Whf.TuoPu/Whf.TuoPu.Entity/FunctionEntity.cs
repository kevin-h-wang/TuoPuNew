using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whf.TuoPu.Entity
{
    public class FunctionEntity
    {
        private string m_OID;
		private int m_FUNCTIONLEVEL;
		private string m_FUNCTIONPARENTID;
		private string m_FUNCTIONURL;
		private string m_FUNCTIONSTATUS;
		private int m_FUNCTIONORDER;
		private int m_FUNCTIONTYPE;
		private string m_FUNCTIONKEY;
		private string m_FUNCTIONNAME;
		private string m_MEMO;
		private string m_CUSER;
        private DateTime m_CDATE;
		private string m_MUSER;
        private DateTime m_MDATE;

		/// <summary>
		/// 构造函数
		/// </summary>
        public FunctionEntity()
		{
		}

		
		/// <summary>
		/// OID
		/// </summary>
		public string OID
		{
			get
			{
				return m_OID ;
			}
			set
			{
				m_OID = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONLEVEL
		/// </summary>
		public int FUNCTIONLEVEL
		{
			get
			{
				return m_FUNCTIONLEVEL ;
			}
			set
			{
				m_FUNCTIONLEVEL = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONPARENTID
		/// </summary>
		public string FUNCTIONPARENTID
		{
			get
			{
				return m_FUNCTIONPARENTID ;
			}
			set
			{
				m_FUNCTIONPARENTID = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONURL
		/// </summary>
		public string FUNCTIONURL
		{
			get
			{
				return m_FUNCTIONURL ;
			}
			set
			{
				m_FUNCTIONURL = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONSTATUS
		/// </summary>
		public string FUNCTIONSTATUS
		{
			get
			{
				return m_FUNCTIONSTATUS ;
			}
			set
			{
				m_FUNCTIONSTATUS = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONORDER
		/// </summary>
		public int FUNCTIONORDER
		{
			get
			{
				return m_FUNCTIONORDER ;
			}
			set
			{
				m_FUNCTIONORDER = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONTYPE
		/// </summary>
		public int FUNCTIONTYPE
		{
			get
			{
				return m_FUNCTIONTYPE ;
			}
			set
			{
				m_FUNCTIONTYPE = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONKEY
		/// </summary>
		public string FUNCTIONKEY
		{
			get
			{
				return m_FUNCTIONKEY ;
			}
			set
			{
				m_FUNCTIONKEY = value ;
			}
		}
		
		/// <summary>
		/// FUNCTIONNAME
		/// </summary>
		public string FUNCTIONNAME
		{
			get
			{
				return m_FUNCTIONNAME ;
			}
			set
			{
				m_FUNCTIONNAME = value ;
			}
		}
		
		/// <summary>
		/// MEMO
		/// </summary>
		public string MEMO
		{
			get
			{
				return m_MEMO ;
			}
			set
			{
				m_MEMO = value ;
			}
		}
		
		/// <summary>
		/// CUSER
		/// </summary>
		public string CUSER
		{
			get
			{
				return m_CUSER ;
			}
			set
			{
				m_CUSER = value ;
			}
		}
		
		/// <summary>
		/// CDATE
		/// </summary>
        public DateTime CDATE
		{
			get
			{
				return m_CDATE ;
			}
			set
			{
				m_CDATE = value ;
			}
		}
		
		/// <summary>
		/// MUSER
		/// </summary>
		public string MUSER
		{
			get
			{
				return m_MUSER ;
			}
			set
			{
				m_MUSER = value ;
			}
		}
		
		/// <summary>
		/// MDATE
		/// </summary>
        public DateTime MDATE
		{
			get
			{
				return m_MDATE ;
			}
			set
			{
				m_MDATE = value ;
			}
		}
    }
}
