using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Whf.TuoPu.DAL;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Controller
{
    public class FunctionController
    {
        #region 操作
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public bool InsertFunction(FunctionEntity fun)
        {
            if (fun != null)
            {
                string strSQL = @" INSERT  INTO TBLFUNCTION
                                            ( oid , functionkey , functionname ,  functionlevel , functionparentid , functionurl ,functionstatus ,
                                              functionorder , functiontype , memo ,cuser ,cdate ,muser ,mdate ,addition1 ,addition2 )
                                    VALUES  ( @OID ,@functionkey ,@functionname ,@functionlevel ,@functionparentid ,@functionurl ,@functionstatus ,
                                              @functionorder ,@functiontype , @memo ,@cuser ,GETDATE() , @muser ,GETDATE() ,NULL ,NULL) ";
                string[] paramNames = new string[12];
                object[] paramValues = new object[12];

                paramNames[0] = "OID";
                paramNames[1] = "functionkey";
                paramNames[2] = "functionname";
                paramNames[3] = "functionlevel";
                paramNames[4] = "functionparentid";
                paramNames[5] = "functionurl";
                paramNames[6] = "functionstatus";
                paramNames[7] = "functionorder";
                paramNames[8] = "functiontype";
                paramNames[9] = "memo";
                paramNames[10] = "cuser";
                paramNames[11] = "muser";

                paramValues[0] = fun.OID;
                paramValues[1] = fun.FUNCTIONKEY;
                paramValues[2] = fun.FUNCTIONNAME;
                paramValues[3] = fun.FUNCTIONLEVEL;
                paramValues[4] = fun.FUNCTIONPARENTID;
                paramValues[5] = fun.FUNCTIONURL;
                paramValues[6] = fun.FUNCTIONSTATUS;
                paramValues[7] = fun.FUNCTIONORDER;
                paramValues[8] = fun.FUNCTIONTYPE;
                paramValues[9] = fun.MEMO;
                paramValues[10] = fun.CUSER;
                paramValues[11] = fun.MUSER;
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
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public bool UpdateFunction(FunctionEntity fun)
        {
            if (fun != null)
            {
                string strSQL = @" UPDATE TBLFUNCTION SET functionkey=@functionkey,functionname=@functionname, functionurl=@functionurl ,
	                                functionstatus=@functionstatus ,functionorder=@functionorder,memo=@memo,muser=@muser,mdate=GETDATE()
	                                WHERE oid=@OID ";
                string[] paramNames = new string[8];
                object[] paramValues = new object[8];

                paramNames[0] = "OID";
                paramNames[1] = "functionkey";
                paramNames[2] = "functionname";
                paramNames[3] = "functionurl";
                paramNames[4] = "functionstatus";
                paramNames[5] = "functionorder";
                paramNames[6] = "memo";
                paramNames[7] = "muser";

                paramValues[0] = fun.OID;
                paramValues[1] = fun.FUNCTIONKEY;
                paramValues[2] = fun.FUNCTIONNAME;
                paramValues[3] = fun.FUNCTIONURL;
                paramValues[4] = fun.FUNCTIONSTATUS;
                paramValues[5] = fun.FUNCTIONORDER;
                paramValues[6] = fun.MEMO;
                paramValues[7] = fun.MUSER;
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="lstOID"></param>
        /// <returns></returns>
        public bool DeleteFunction(List<string> lstOID)
        {
            string strSQL = @" DELETE FROM TBLFUNCTION WHERE oid=@OID ";
            string[] paramNames = new string[1];
            object[] paramValues = new object[1];

            paramNames[0] = "OID";
            SqlDBBroker broker = new SqlDBBroker();
            try
            {
                broker.Open();
                broker.BeginTrans();
                foreach (string oid in lstOID)
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
        public string GetChildMaxOrder(string oid)
        {
            string strSQL = @" SELECT COUNT(1)+1 FROM TBLFUNCTION WHERE functionparentid=@OID ";
            string[] paramNames = new string[1];
            object[] paramValues = new object[1];

            paramNames[0] = "OID";
            paramValues[0] = oid;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            return broker.ExecuteScalar(strSQL, CommandType.Text, paramNames, paramValues);
        }

        public FunctionEntity GetFunc(string oid)
        {
            string strSQL = @" SELECT * FROM TBLFUNCTION WHERE oid=@OID ";
            string[] paramNames = new string[1];
            object[] paramValues = new object[1];

            paramNames[0] = "OID";
            paramValues[0] = oid;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.ExecuteDataset(strSQL,CommandType.Text,paramNames,paramValues);
            broker.Close();
            if (dst != null && dst.Tables[0] != null && dst.Tables[0].Rows.Count > 0)
            {
                return Datarow2Entity(dst.Tables[0].Rows[0]);
            }
            else
            { return null; }
        }

        private FunctionEntity Datarow2Entity(DataRow dr)
        {
            if (dr != null)
            {
                FunctionEntity fun = new FunctionEntity();
                fun.OID = Convert.ToString(dr["OID"]);
                fun.FUNCTIONKEY = Convert.ToString(dr["FUNCTIONKEY"]);
                fun.FUNCTIONLEVEL = Convert.ToInt32(dr["FUNCTIONLEVEL"]);
                fun.FUNCTIONNAME = Convert.ToString(dr["FUNCTIONNAME"]);
                fun.FUNCTIONORDER = Convert.ToInt32(dr["FUNCTIONORDER"]);
                fun.FUNCTIONPARENTID = Convert.ToString(dr["FUNCTIONPARENTID"]);
                fun.FUNCTIONSTATUS = Convert.ToString(dr["FUNCTIONSTATUS"]);
                fun.FUNCTIONTYPE = Convert.ToInt32(dr["FUNCTIONTYPE"]);
                fun.FUNCTIONURL = Convert.ToString(dr["FUNCTIONURL"]);
                fun.MEMO = Convert.ToString(dr["MEMO"]);
                return fun;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查询功能列表
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="funcName"></param>
        /// <returns></returns>
        public DataSet QueryFunctions(string funcCode,string funcName)
        {
            string strSql = @" SELECT  *
                            FROM    dbo.TBLFUNCTION 
                            WHERE   1 = 1 ";
            if (!string.IsNullOrEmpty(funcCode))
            {
                strSql += " and p.functionkey = FunCode ";
            }
            if (!string.IsNullOrEmpty(funcName))
            {
                strSql += " and p.functionname = FunName ";
            }
            strSql += " ORDER BY functionorder ";
            string[] paramNames = new string[2];
            object[] paramValues = new object[2];

            paramNames[0] = "FunCode";
            paramNames[1] = "FunName";

            paramValues[0] = funcCode;
            paramValues[1] = funcName;
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.ExecuteDataset(strSql);
            broker.Close();
            return dst;
        }

        public DataSet GetAllFunctions()
        {
            string strSql = " SELECT * FROM TBLFUNCTION  ORDER BY functionorder ";
            SqlDBBroker broker = new SqlDBBroker();
            broker.Open();
            DataSet dst = broker.ExecuteDataset(strSql);
            broker.Close();
            return dst;
        }
        #endregion
    }
}
