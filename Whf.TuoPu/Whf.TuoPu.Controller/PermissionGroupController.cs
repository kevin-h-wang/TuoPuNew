using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Whf.TuoPu.DAL;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Controller
{
    public class PermissionGroupController
    {
        #region 操作
        public bool InsertGroup(PermissionGroupEntity group)
        {
            if (group != null)
            {
                string strSQL = @" INSERT INTO dbo.TBLPermissionGroup( oid , groupcode , groupname , groupstatus ,memo,
                                              cuser , cdate , muser ,  mdate , addition1 , addition2 )
                                    VALUES  ( @OID , @GroupCode , @GroupName , @GroupStatus,@Memo,
                                              @Cuser , GETDATE(), @Muser,GETDATE(),NULL, NULL) ";
                string[] paramNames = new string[7];
                object[] paramValues = new object[7];

                paramNames[0] = "OID";
                paramNames[1] = "GroupCode";
                paramNames[2] = "GroupName";
                paramNames[3] = "GroupStatus";
                paramNames[4] = "Memo";
                paramNames[5] = "Cuser";
                paramNames[6] = "Muser";

                paramValues[0] = group.OID;
                paramValues[1] = group.GroupCode;
                paramValues[2] = group.GroupName;
                paramValues[3] = group.GroupStatus;
                paramValues[4] = group.MEMO;
                paramValues[5] = group.CUSER;
                paramValues[6] = group.MUSER;
                SqlDBBroker broker = new SqlDBBroker();
                try
                {
                    broker.Open();
                    broker.BeginTrans();
                    broker.ExecuteNonQuery(strSQL, CommandType.Text, paramNames, paramValues);
                    broker.CommitTrans();
                }
                catch
                {
                    broker.RollbackTrans();
                    return false;
                }
                finally
                {
                    broker.Close();
                }
            }
            return true;
        }

        public bool UpdateGroup(PermissionGroupEntity group)
        {
            if (group != null)
            {
                string strSQL = @" UPDATE TBLPermissionGroup SET groupcode=@GroupCode,groupname=@GroupName,groupstatus=@GroupStatus,
			                                    memo=@Memo,muser=@Muser,mdate=GETDATE() WHERE oid=@OID   ";
                string[] paramNames = new string[6];
                object[] paramValues = new object[6];

                paramNames[0] = "OID";
                paramNames[1] = "GroupCode";
                paramNames[2] = "GroupName";
                paramNames[3] = "GroupStatus";
                paramNames[4] = "Memo";
                paramNames[5] = "Muser";

                paramValues[0] = group.OID;
                paramValues[1] = group.GroupCode;
                paramValues[2] = group.GroupName;
                paramValues[3] = group.GroupStatus;
                paramValues[4] = group.MEMO;
                paramValues[5] = group.MUSER;
                SqlDBBroker broker = new SqlDBBroker();
                try
                {
                    broker.Open();
                    broker.BeginTrans();
                    broker.ExecuteNonQuery(strSQL, CommandType.Text, paramNames, paramValues);
                    broker.CommitTrans();
                }
                catch
                {
                    broker.RollbackTrans();
                    return false;
                }
                finally
                {
                    broker.Close();
                }
            }
            return true;
        }

        public bool DeletePermissionGroup(List<string> groupIDs)
        {
            string strSQL = @" DELETE FROM TBLPermissionGroup WHERE oid=@OID   ";
            string[] paramNames = new string[1];
            object[] paramValues = new object[1];
            SqlDBBroker broker = new SqlDBBroker();
            try
            {
                broker.Open();
                broker.BeginTrans();
                paramNames[0] = "OID";
                foreach (string oid in groupIDs)
                {
                    paramValues[0] = oid;
                    broker.ExecuteNonQuery(strSQL, CommandType.Text, paramNames, paramValues);
                }
                broker.CommitTrans();
            }
            catch
            {
                broker.RollbackTrans();
                return false;
            }
            finally
            {
                broker.Close();
            }
            return true;
        }
        #endregion

        #region 查询
        public PermissionGroupEntity GetGroupInfo(string personID)
        {
            PermissionGroupEntity pe = new PermissionGroupEntity();
            string strSql = @" SELECT * FROM TBLPermissionGroup WHERE oid=@OID  ";
            string[] paramName = new string[1];
            object[] paramValue = new object[1];
            paramName[0] = "OID";

            paramValue[0] = personID;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.ExecuteDataset(strSql, CommandType.Text, paramName, paramValue);
            broker.Close();
            if (dst != null && dst.Tables[0] != null && dst.Tables[0].Rows.Count > 0)
            {
                return this.DataRow2Group(dst.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        private PermissionGroupEntity DataRow2Group(DataRow dr)
        {
            PermissionGroupEntity group = new PermissionGroupEntity();
            if (dr != null)
            {
                group.OID = Convert.ToString(dr["oid"]);
                group.GroupCode = Convert.ToString(dr["groupcode"]);
                group.GroupName = Convert.ToString(dr["groupname"]);
                group.GroupStatus = Convert.ToInt32(dr["groupstatus"]);
                group.MEMO = Convert.ToString(dr["memo"]);
            }
            return group;
        }

        public DataSet QueryGroupPermissions(int pageIndex, int pageSize, out int rowCount, string groupCode, string groupName)
        {
            string strSql = @" SELECT * FROM TBLPermissionGroup WHERE 1=1 ";
            if (!string.IsNullOrEmpty(groupCode))
            {
                strSql += " and groupcode like @GroupCode ";
            }
            if (!string.IsNullOrEmpty(groupName))
            {
                strSql += " and groupname like @GroupName ";
            }
            string[] paramNames = new string[2];
            object[] paramValues = new object[2];

            paramNames[0] = "GroupCode";
            paramNames[1] = "GroupName";

            paramValues[0] = "%" + groupCode + "%";
            paramValues[1] = "%" + groupName + "%";
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.QueryPageFromSql(strSql, paramNames, paramValues, pageIndex, pageSize, out rowCount);
            broker.Close();
            return dst;
        }
        #endregion
    }
}
