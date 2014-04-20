using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whf.TuoPu.Entity
{
    public class PermissionGroupEntity
    {
        private string m_OID;
        private string m_groupcode;
        private string m_groupname;
        private int m_groupstatus;
        private string m_memo;
		private string m_CUSER;
		private DateTime m_CDATE;
		private string m_MUSER;
        private DateTime m_MDATE;

		/// <summary>
		/// 构造函数
		/// </summary>
        public PermissionGroupEntity()
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
		/// FUNCTIONPARENTID
		/// </summary>
		public string GroupCode
		{
			get
			{
                return m_groupcode;
			}
			set
			{
                m_groupcode = value;
			}
		}
		
		/// <summary>
		/// FUNCTIONURL
		/// </summary>
		public string GroupName
		{
			get
			{
                return m_groupname;
			}
			set
			{
                m_groupname = value;
			}
		}
		
		/// <summary>
		/// FUNCTIONSTATUS
		/// </summary>
		public int GroupStatus
		{
			get
			{
                return m_groupstatus;
			}
			set
			{
                m_groupstatus = value;
			}
		}
		
		
		/// <summary>
		/// MEMO
		/// </summary>
		public string MEMO
		{
			get
			{
                return m_memo;
			}
			set
			{
                m_memo = value;
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
