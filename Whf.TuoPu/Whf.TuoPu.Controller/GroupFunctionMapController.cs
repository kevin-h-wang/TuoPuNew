using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Whf.TuoPu.DAL;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Controller
{
    public class GroupFunctionMapController
    {
        /// <summary>
        /// 根据群组查询权限
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public DataTable GetFuncByGroup(string groupID)
        {
            string strSQL = @" SELECT * FROM dbo.TBLGroupFunctionMap WHERE groupID=@GroupID ";
            string[] paramNames = new string[1];
            object[] paramValues = new object[1];

            paramNames[0] = "GroupID";
            paramValues[0] = groupID;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.ExecuteDataset(strSQL, CommandType.Text, paramNames, paramValues);
            broker.Close();
            return dst.Tables[0];
        }

        /// <summary>
        /// 保存功能组菜单权限
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool SaveGroupFunction(List<GroupFunctionMapEntity> entitys)
        {
            if (entitys.Count > 0)
            {
                SqlDBBroker broker = new SqlDBBroker();
                try
                {
                    broker.Open();
                    broker.BeginTrans();

                    string strDelSQL = " DELETE FROM dbo.TBLGroupFunctionMap WHERE groupID=@GroupID ";
                    string[] paramNames1 = new string[1];
                    object[] paramValues1 = new object[1];

                    paramNames1[0] = "GroupID";
                    paramValues1[0] = entitys[0].GroupID;
                    broker.ExecuteNonQuery(strDelSQL, CommandType.Text, paramNames1, paramValues1);

                    string strSQL = @" INSERT INTO dbo.TBLGroupFunctionMap
                                            ( oid , groupID , functionID , cuser , cdate , muser , mdate , addition1 , addition2 )
                                    VALUES  ( @OID,@GroupID ,@FunctionID,@Cuser,getdate(),@Muser ,getdate() ,NULL,NULL) ";
                    string[] paramNames = new string[5];
                    object[] paramValues = new object[5];

                    foreach (GroupFunctionMapEntity en in entitys)
                    {
                        paramNames[0] = "OID";
                        paramNames[1] = "GroupID";
                        paramNames[2] = "FunctionID";
                        paramNames[3] = "Cuser";
                        paramNames[4] = "Muser";

                        paramValues[0] = en.OID;
                        paramValues[1] = en.GroupID;
                        paramValues[2] = en.FunctionID;
                        paramValues[3] = en.CUSER;
                        paramValues[4] = en.MUSER;

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
            }
            return true;
        }
    }
}
