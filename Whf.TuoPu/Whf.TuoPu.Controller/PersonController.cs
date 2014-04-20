using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Whf.TuoPu.DAL;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Controller
{
    public class PersonController
    {
        #region 操作
        public bool InsertPerson(PersonEntity per)
        {
            if (per != null)
            {
                string strSQL = @" INSERT INTO TBLPERSON ( oid , personcode , personaccount , personpassword , personname , personsex ,
						                        personstatus , persontype , personofficephone , personmobilephone , personemail ,
						                        personmemo , cuser , cdate , muser , mdate , mpwdtime , personpasswordquestion , 
						                        personpasswordanswer , addition1 , addition2 )
			                        VALUES  ( @oid, @personcode,@personaccount,@personpassword,@personname,@personsex,
						                        @personstatus,@persontype,@personofficephone,@personmobilephone,@personemail,
						                        @personmemo,@cuser, GETDATE(),@muser,GETDATE(),GETDATE(),' ' ,' ', NULL,NULL) ";
                string[] paramNames = new string[14];
                object[] paramValues = new object[14];

                paramNames[0] = "OID";
                paramNames[1] = "personcode";
                paramNames[2] = "personaccount";
                paramNames[3] = "personpassword";
                paramNames[4] = "personname";
                paramNames[5] = "personsex";
                paramNames[6] = "personstatus";
                paramNames[7] = "persontype";
                paramNames[8] = "personofficephone";
                paramNames[9] = "personmobilephone";
                paramNames[10] = "personemail";
                paramNames[11] = "personmemo";
                paramNames[12] = "cuser";
                paramNames[13] = "muser";

                paramValues[0] = per.OID;
                paramValues[1] = per.PERSONCODE;
                paramValues[2] = per.PERSONACCOUNT;
                paramValues[3] = per.PERSONPASSWORD??" ";
                paramValues[4] = per.PERSONNAME;
                paramValues[5] = per.PERSONSEX;
                paramValues[6] = per.PERSONSTATUS;
                paramValues[7] = per.PERSONTYPE;
                paramValues[8] = per.PERSONOFFICEPHONE;
                paramValues[9] = per.PERSONMOBILEPHONE;
                paramValues[10] = per.PERSONEMAIL;
                paramValues[11] = per.PERSONMEMO;
                paramValues[12] = per.CUSER;
                paramValues[13] = per.MUSER;
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

        public bool UpdatePersonAdmin(PersonEntity per)
        {
            if (per != null)
            {
                string strSQL = @" UPDATE TBLPERSON SET personcode=@personcode,personaccount=@personaccount,personname=@personname,personsex=@personsex,
                                    personstatus=@personstatus,persontype=@persontype,personofficephone=@personofficephone,
                                    personmobilephone=@personmobilephone,personemail=@personemail,personmemo=@personmemo,muser=@muser,mdate=GETDATE()
                                    WHERE oid=@oid ";
                string[] paramNames = new string[12];
                object[] paramValues = new object[12];

                paramNames[0] = "OID";
                paramNames[1] = "personcode";
                paramNames[2] = "personaccount";
                paramNames[3] = "personname";
                paramNames[4] = "personsex";
                paramNames[5] = "personstatus";
                paramNames[6] = "persontype";
                paramNames[7] = "personofficephone";
                paramNames[8] = "personmobilephone";
                paramNames[9] = "personemail";
                paramNames[10] = "personmemo";
                paramNames[11] = "muser";

                paramValues[0] = per.OID;
                paramValues[1] = per.PERSONCODE;
                paramValues[2] = per.PERSONACCOUNT;
                paramValues[3] = per.PERSONNAME;
                paramValues[4] = per.PERSONSEX;
                paramValues[5] = per.PERSONSTATUS;
                paramValues[6] = per.PERSONTYPE;
                paramValues[7] = per.PERSONOFFICEPHONE;
                paramValues[8] = per.PERSONMOBILEPHONE;
                paramValues[9] = per.PERSONEMAIL;
                paramValues[10] = per.PERSONMEMO;
                paramValues[11] = per.MUSER;
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

        public bool DeletePersons(List<string> personIDs)
        {
            string strSQL = @" DELETE FROM TBLPERSON WHERE oid=@OID  ";
            string[] paramNames = new string[1];
            object[] paramValues = new object[1];
            SqlDBBroker broker = new SqlDBBroker();
            try
            {
                broker.Open();
                broker.BeginTrans();
                paramNames[0] = "OID";
                foreach (string oid in personIDs)
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
        public PersonEntity GetPersonInfo(string personID)
        {
            PersonEntity pe = new PersonEntity();
            string strSql = @" SELECT  *  
                                FROM    TBLPERSON where oid=@OID ";
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
                return this.DataRow2Person(dst.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public PersonEntity GetPersonInfo(string userName, string passWord)
        {
            PersonEntity pe = new PersonEntity();
            string strSql = @" SELECT  * 
                                FROM    TBLPERSON where personaccount=@UserName and personpassword=@PassWord";
            string[] paramName=new string[2];
            object[] paramValue = new object[2];
            paramName[0]="UserName";
            paramName[1] = "PassWord";

            paramValue[0] = userName;
            paramValue[1] = passWord;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.ExecuteDataset(strSql,CommandType.Text,paramName,paramValue);
            broker.Close();
            if (dst != null && dst.Tables[0] != null && dst.Tables[0].Rows.Count > 0)
            {
                return this.DataRow2Person(dst.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        private PersonEntity DataRow2Person(DataRow dr)
        {
            PersonEntity pe = new PersonEntity();
            if (dr != null)
            {
                pe.OID = Convert.ToString(dr["oid"]);
                pe.PERSONACCOUNT = Convert.ToString(dr["personaccount"]);
                pe.PERSONNAME = Convert.ToString(dr["personname"]);
                pe.PERSONSEX = Convert.ToInt32(dr["personsex"]);
                pe.PERSONSTATUS = Convert.ToInt32(dr["personstatus"]);
                pe.PERSONTYPE = Convert.ToInt32(dr["persontype"]);
                pe.PERSONEMAIL = Convert.ToString(dr["personemail"]);
                pe.PERSONMOBILEPHONE = Convert.ToString(dr["personmobilephone"]);
                pe.PERSONOFFICEPHONE = Convert.ToString(dr["personofficephone"]);
                pe.PERSONMEMO = Convert.ToString(dr["personmemo"]);
            }
            return pe;
        }

        public DataSet QueryPersons(int pageIndex,int pageSize,out int rowCount,string userAccount,string userName,string personType)
        {
            string strSql = @" SELECT  oid ,
                                        personaccount ,
                                        personname ,
                                        personsex ,
                                        personstatus,
                                        personofficephone ,
                                        personmobilephone ,
                                        personemail ,
                                        personmemo,persontype 
                                FROM    TBLPERSON where 1=1 ";
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
            string[] paramNames = new string[3];
            object[] paramValues = new object[3];

            paramNames[0] = "PersonAccount";
            paramNames[1] = "PersonName";
            paramNames[2] = "PersonType";

            paramValues[0] = "%" + userAccount + "%";
            paramValues[1] = "%" + userName + "%";
            paramValues[2] = personType;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.QueryPageFromSql(strSql,paramNames,paramValues,pageIndex,pageSize,out rowCount);
            broker.Close();
            return dst;
        }

        #endregion
    }
}
