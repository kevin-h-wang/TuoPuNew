using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whf.TuoPu.DAL;
using System.Data;

namespace Whf.TuoPu.Controller
{
    public class GroupPersonMapController
    {
        /// <summary>
        /// 查询群组人员
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="userAccount"></param>
        /// <param name="userName"></param>
        /// <param name="personType"></param>
        /// <returns></returns>
        public DataSet QueryPersons(int pageIndex, int pageSize, out int rowCount, string userAccount, string userName, string personType, string groupID)
        {
            string strSql = @" SELECT  TBLPERSON.oid ,
                                        TBLPERSON.personaccount ,
                                        TBLPERSON.personname ,
                                        TBLPERSON.personsex ,
                                        TBLPERSON.personstatus ,
                                        TBLPERSON.personofficephone ,
                                        TBLPERSON.personmobilephone ,
                                        TBLPERSON.personemail ,
                                        TBLPERSON.personmemo ,
                                        persontype
                                FROM    TBLGroupPersonMap
                                        INNER JOIN TBLPERSON ON TBLGroupPersonMap.PersonID = TBLPERSON.oid
                                WHERE   1 = 1 ";
            if (!string.IsNullOrEmpty(userAccount))
            {
                strSql += " and personaccount like @PersonAccount ";
            }
            if (!string.IsNullOrEmpty(userName))
            {
                strSql += " and personname like @PersonName ";
            }
            if (!string.IsNullOrEmpty(personType))
            {
                strSql += " and persontype = @PersonType ";
            }
            if (!string.IsNullOrEmpty(groupID))
            {
                strSql += " AND TBLGroupPersonMap.groupID=@GroupID ";
            }
            string[] paramNames = new string[4];
            object[] paramValues = new object[4];

            paramNames[0] = "PersonAccount";
            paramNames[1] = "PersonName";
            paramNames[2] = "PersonType";
            paramNames[3] = "GroupID";

            paramValues[0] = "%" + userAccount + "%";
            paramValues[1] = "%" + userName + "%";
            paramValues[2] = personType;
            paramValues[3] = groupID;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.QueryPageFromSql(strSql, paramNames, paramValues, pageIndex, pageSize, out rowCount);
            broker.Close();
            return dst;
        }

        /// <summary>
        /// 保存功能组人员
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool SaveGroupPerson(List<string> personIDS,string groupID,string editor)
        {
            if (personIDS.Count > 0)
            {
                SqlDBBroker broker = new SqlDBBroker();
                try
                {
                    broker.Open();
                    broker.BeginTrans();

                    string strDelSQL = " DELETE FROM dbo.TBLGroupPersonMap WHERE groupID=@GroupID ";
                    string[] paramNames1 = new string[1];
                    object[] paramValues1 = new object[1];

                    paramNames1[0] = "GroupID";
                    paramValues1[0] = groupID;
                    broker.ExecuteNonQuery(strDelSQL, CommandType.Text, paramNames1, paramValues1);

                    string strSQL = @" INSERT INTO dbo.TBLGroupPersonMap
                                            ( oid , groupID , personID , cuser , cdate , muser , mdate , addition1 , addition2 )
                                    VALUES  ( @OID,@GroupID ,@PersonID,@Cuser,getdate(),@Muser ,getdate() ,NULL,NULL) ";
                    string[] paramNames = new string[5];
                    object[] paramValues = new object[5];

                    foreach (string personid in personIDS)
                    {
                        paramNames[0] = "OID";
                        paramNames[1] = "GroupID";
                        paramNames[2] = "PersonID";
                        paramNames[3] = "Cuser";
                        paramNames[4] = "Muser";

                        paramValues[0] = Guid.NewGuid().ToString();
                        paramValues[1] = groupID;
                        paramValues[2] = personid;
                        paramValues[3] = editor;
                        paramValues[4] = editor;

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

        /// <summary>
        /// 删除群组下的人
        /// </summary>
        /// <param name="personIDS"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public bool DeleteGroupPersonByPersonID(List<string> personIDS,string groupID)
        {
            if (personIDS.Count > 0)
            {
                SqlDBBroker broker = new SqlDBBroker();
                try
                {
                    broker.Open();
                    broker.BeginTrans();

                    string strDelSQL = " DELETE FROM dbo.TBLGroupPersonMap WHERE PersonID=@PersonID AND groupID=@GroupID ";
                    string[] paramNames = new string[2];
                    object[] paramValues = new object[2];

                    paramNames[0] = "GroupID";
                    paramNames[1] = "PersonID";
                    paramValues[0] = groupID;
                    foreach (string personid in personIDS)
                    {
                        paramValues[1] = personid;
                        broker.ExecuteNonQuery(strDelSQL, CommandType.Text, paramNames, paramValues);
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
