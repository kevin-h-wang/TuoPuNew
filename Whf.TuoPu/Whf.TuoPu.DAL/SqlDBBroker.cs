
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Whf.TuoPu.Common;

namespace Whf.TuoPu.DAL
{
    public class SqlDBBroker
    {
        private SqlConnection conn;

        private bool IsInTransaction = false;   //指示当前是否正处于事务中
        private SqlTransaction trans;           //事务处理类

        private int m_timeout = 30;
        /// <summary>
        /// Sql语句执行时间
        /// </summary>
        public int TimeOut
        {
            get
            {
                return m_timeout;
            }
            set
            {
                if (m_timeout <= 0)
                {
                    m_timeout = 30;
                }
                else
                {
                    m_timeout = value;
                }
            }
        }

        #region 获取一个实例
        public SqlDBBroker()
        {
            try
            {
                string connectionString = "";
                ConfigManager cm = (ConfigManager)Thread.GetDomain().GetData("ConfigManager");

                if (cm == null)
                {
                    throw new Exception("Palau配置管理器未在启动时加载成功!");
                }
                //读取数据库连接字符串
                connectionString = cm["ConnectionString"].ToString();

                //进行字符串的解密（默认情况下连接字符串是加密的）
                connectionString = WhfEncryption.DESDeCrypt(connectionString);
                this.conn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 打开关闭连接
        public void Open()
        {
            if (conn.State.ToString().ToUpper() != "OPEN")
            {
                this.conn.Open();
            }
        }

        public void Close()
        {
            if (conn.State.ToString().ToUpper() == "OPEN")
            {
                this.conn.Close();
            }
        }
        #endregion

        #region 判断事物
        private int _translevel = 0;
        private bool IsBeginTrans()
        {
            //
            this._translevel++;
            if (this._translevel != 1)
                return false;
            else
                return true;
        }

        private bool IsCommitTrans()
        {
            if (this._translevel == 0)
            {
                return true;
            }
            else
            {
                this._translevel--;
                if (this._translevel == 0)
                    return true;
                else
                    return false;
            }
        }

        private bool IsRollBack()
        {
            this._translevel = 0;
            return true;
        }
        #endregion

        #region 事务处理
        public void BeginTrans()
        {
            if (this.IsBeginTrans())
            {
                try
                {
                    //开始事务前先打开连接
                    if ((conn != null) && (conn.State == ConnectionState.Closed))
                    {
                        conn.Open();
                    }
                    //已经开始的事务不允许再并行
                    if (!IsInTransaction)
                    {
                        trans = conn.BeginTransaction();
                        IsInTransaction = true;
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void CommitTrans()
        {
            if (this.IsCommitTrans())
            {
                try
                {
                    //已经开始的事务才可以提交
                    if (IsInTransaction)
                    {
                        trans.Commit();
                        IsInTransaction = false;
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void RollbackTrans()
        {
            this.IsRollBack();
            try
            {
                //已经开始的事务才可以取消
                if (IsInTransaction)
                {
                    trans.Rollback();
                    IsInTransaction = false;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 准备Command事物连接
        /// <summary>
        /// 准备好Command的数据库连接,事务,参数
        /// </summary>
        /// <param name="command">传入的Command</param>
        /// <param name="Sql">SQL语句</param>
        /// <param name="sqlType">CommandType类型</param>
        /// <param name="sqlParams">参数名称集合</param>
        /// <param name="paramValues">参数值集合</param>
        private void PrepareCommand(SqlCommand command, string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues)
        {
            //先打开连接
            if (this.conn.State != ConnectionState.Open)
            {
                this.conn.Open();
            }
            //把词数据库连接赋予cmd
            command.Connection = conn;

            //判断是否支持事务
            if (IsInTransaction)
            {
                command.Transaction = this.trans;
            }

            //判断参数名称与参数值是否一一对应
            if ((sqlParams != null) && (sqlParams.Length != paramValues.Length))
            {
                throw new ApplicationException("Param Value not Match");
            }

            //把SQL语句与参数赋予Command
            command.CommandText = Sql;
            command.CommandType = sqlType;

            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    if (sqlParams[i] != "")
                    {
                        command.Parameters.Add(sqlParams[i], paramValues[i]);
                    }
                }
            }
            return;
        }

        #endregion

        #region 对数据库的数据进行操作
        public void ExecuteNonQuery(string Sql)
        {
            //把null转换成数组,调用已经实现的方法
            this.ExecuteNonQuery(Sql, CommandType.Text, new string[0], new object[0]);
        }

        public void ExecuteNonQuery(string Sql, CommandType sqlType, string sqlParam, object paramValue)
        {
            //把参数变量转换成数组,再调用已经实现的方法
            string[] sqlParams = new string[1];
            object[] paramValues = new object[1];
            sqlParams[0] = sqlParam;
            paramValues[0] = paramValue;

            this.ExecuteNonQuery(Sql, sqlType, sqlParams, paramValues);

        }

        public void ExecuteNonQuery(string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues)
        {
            try
            {
                //生成新的Command对象
                SqlCommand cmd = new SqlCommand();

                //准备好Command
                this.PrepareCommand(cmd, Sql, sqlType, sqlParams, paramValues);

                //添加sql语句执行时间
                cmd.CommandTimeout = this.TimeOut;

                //执行修改数据库中数据的命令
                cmd.ExecuteNonQuery();

                //为了下次使用Command,清除其中的参数
                cmd.Parameters.Clear();
            }
            catch (System.Exception ex)
            {
                throw new Exception("SourceSQL:" + Sql + "  |  ProcessedSQL:" + Sql + "  |  ErrorMessage:" + ex.Message);

            }
            finally
            {
            }
        }

        public void ExecuteProcedure(string ProcedureName, CommandType Type, string[] ParamNames, object[] ParamValues)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = ProcedureName;
                cmd.CommandType = Type;

                if (this.conn.State != ConnectionState.Open)
                {
                    this.conn.Open();
                }

                cmd.Connection = this.conn;

                if (this.IsInTransaction)
                {
                    cmd.Transaction = this.trans;
                }

                //准备入参
                SqlParameter objParameter = null;
                for (int i = 0; i < ParamNames.Length; i++)
                {
                    objParameter = new SqlParameter();
                    objParameter.ParameterName = ParamNames[i];
                    objParameter.Value = ParamValues[i];
                    cmd.Parameters.Add(objParameter);
                }

                //添加sql语句执行时间
                cmd.CommandTimeout = this.TimeOut;

                //执行修改数据库中数据的命令
                cmd.ExecuteNonQuery();

                //为了下次使用Command,清除其中的参数
                cmd.Parameters.Clear();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Execute SQLSERVER Store Procedure Failed! Procedure Name : " + ProcedureName + "  Error : " + ex.Message);
            }
        }
        #endregion

        #region 执行查询返回数据集
        public DataSet ExecuteDataset(string Sql)
        {
            return this.ExecuteDataset(Sql, CommandType.Text, new string[0], new object[0], null);
        }

        public DataSet ExecuteDataset(string Sql, string tableName)
        {
            return this.ExecuteDataset(Sql, CommandType.Text, new string[0], new object[0], tableName);
        }

        public DataSet ExecuteDataset(string Sql, CommandType sqlType, string sqlParam, object paramValue, string tableName)
        {
            //把参数变量转换成数组,再调用已经实现的方法
            string[] sqlParams = new string[1];
            object[] paramValues = new object[1];
            sqlParams[0] = sqlParam;
            paramValues[0] = paramValue;

            return this.ExecuteDataset(Sql, sqlType, sqlParams, paramValues, tableName);
        }

        public DataSet ExecuteDataset(string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues, string tableName)
        {
            //当参数entityData 为空时候,需要新建一个DataSet
            DataSet entityData;
            if ((tableName != null) && (tableName != ""))
            {
                entityData = new DataSet(tableName);
            }
            else
            {
                entityData = new DataSet();
            }

            try
            {
                //准备好Command
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, Sql, sqlType, sqlParams, paramValues);

                //添加sql语句执行时间
                cmd.CommandTimeout = this.TimeOut;

                //向entityData中填充数据
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;

                //当表名称存在时候,需要建立映射
                if ((tableName != null) && (tableName != ""))
                {
                    ad.Fill(entityData, tableName);
                }
                else
                {
                    ad.Fill(entityData);
                }

                //为了下次使用Command,清除其中的参数
                cmd.Parameters.Clear();

                return entityData;
            }
            catch (System.Exception ex)
            {
                throw new Exception("SourceSQL:" + Sql + "  |  ProcessedSQL:" + Sql + "  |  ErrorMessage:" + ex.Message);
            }
            finally
            {
            }
        }

        public DataSet ExecuteDataset(string Sql, CommandType sqlType, string sqlParam, object paramValue)
        {
            //把参数变量转换成数组,再调用已经实现的方法
            string[] sqlParams = new string[1];
            object[] paramValues = new object[1];
            sqlParams[0] = sqlParam;
            paramValues[0] = paramValue;

            return this.ExecuteDataset(Sql, sqlType, sqlParams, paramValues, null);
        }

        public DataSet ExecuteDataset(string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues)
        {
            return this.ExecuteDataset(Sql, sqlType, sqlParams, paramValues, null);
        }
        #endregion

        #region 查询返回单个值
        public string ExecuteScalar(string Sql)
        {
            return this.ExecuteScalar(Sql, CommandType.Text, new string[0], new object[0]);
        }

        public string ExecuteScalar(string Sql, CommandType sqlType, string sqlParam, object paramValue)
        {
            //把参数变量转换成数组,再调用已经实现的方法
            string[] sqlParams = new string[1];
            object[] paramValues = new object[1];
            sqlParams[0] = sqlParam;
            paramValues[0] = paramValue;

            return this.ExecuteScalar(Sql, sqlType, sqlParams, paramValues);
        }

        public string ExecuteScalar(string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues)
        {
            try
            {
                //准备好Command
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, Sql, sqlType, sqlParams, paramValues);

                //添加sql语句执行时间
                cmd.CommandTimeout = this.TimeOut;
                return cmd.ExecuteScalar().ToString();
            }
            catch (System.Exception ex)
            {
                throw new Exception("SourceSQL:" + Sql + "  |  ProcessedSQL:" + Sql + "  |  ErrorMessage:" + ex.Message);
            }
            finally
            {
            }
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqlPhase">sql string</param>
        /// <param name="paramsName">参数名称列表</param>
        /// <param name="paramsValue">参数值列表</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="rowCount">总行数,要回传</param>
        /// <returns></returns>
        public DataSet QueryPageFromSql(string strSql, string[] paramsName, object[] paramsValue, int pageIndex, int pageSize, out int rowCount)
        {

            //取得行数
            rowCount = int.Parse(this.ExecuteDataset(RowCountSql(strSql), CommandType.Text, paramsName, paramsValue).Tables[0].Rows[0][0].ToString());

            strSql = PageSql(strSql, pageIndex, pageSize);

            DataSet dsItems = this.ExecuteDataset(strSql, CommandType.Text, paramsName, paramsValue);

            return dsItems;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqlPhase">sql string</param>        
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="rowCount">总行数,要回传</param>
        /// <returns></returns>
        public DataSet QueryPageFromSql(string strSql, int pageIndex, int pageSize, out int rowCount)
        {

            //取得行数
            rowCount = int.Parse(this.ExecuteDataset(RowCountSql(strSql)).Tables[0].Rows[0][0].ToString());

            strSql = PageSql(strSql, pageIndex, pageSize);

            DataSet dsItems = this.ExecuteDataset(strSql);

            return dsItems;
        }

        private string RowCountSql(string sql)
        {
            return @"SELECT COUNT(*) FROM (" + sql + @") tablePageCustom";
        }

        private string PageSql(string sql, int pageIndex, int pageSize)
        {
            return @"SELECT * FROM
                            (
                            select row_number()over(order by tempColumn)rownumber,*
                            from (select top " + pageSize * pageIndex + @" tempColumn=0,* from (" + sql + @") tempTable) result2
                            ) result2
                            WHERE rownumber >= " + ((pageIndex - 1) * pageSize + 1);

        }
        #endregion
    }
}
