using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whf.TuoPu.Entity
{
    public class GroupFunctionMapEntity
    {
        private string m_OID;
        private string m_groupID;
        private string m_functionID;
		private string m_CUSER;
        private DateTime m_CDATE;
		private string m_MUSER;
        private DateTime m_MDATE;

		/// <summary>
		/// 构造函数
		/// </summary>
        public GroupFunctionMapEntity()
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
		public string GroupID
		{
			get
			{
                return m_groupID;
			}
			set
			{
                m_groupID = value;
			}
		}
		
		/// <summary>
		/// FUNCTIONPARENTID
		/// </summary>
		public string FunctionID
		{
			get
			{
                return m_functionID;
			}
			set
			{
                m_functionID = value;
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
