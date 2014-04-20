
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

        private bool IsInTransaction = false;   //ָʾ��ǰ�Ƿ�������������
        private SqlTransaction trans;           //��������

        private int m_timeout = 30;
        /// <summary>
        /// Sql���ִ��ʱ��
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

        #region ��ȡһ��ʵ��
        public SqlDBBroker()
        {
            try
            {
                string connectionString = "";
                ConfigManager cm = (ConfigManager)Thread.GetDomain().GetData("ConfigManager");

                if (cm == null)
                {
                    throw new Exception("Palau���ù�����δ������ʱ���سɹ�!");
                }
                //��ȡ���ݿ������ַ���
                connectionString = cm["ConnectionString"].ToString();

                //�����ַ����Ľ��ܣ�Ĭ������������ַ����Ǽ��ܵģ�
                connectionString = WhfEncryption.DESDeCrypt(connectionString);
                this.conn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region �򿪹ر�����
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

        #region �ж�����
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

        #region ������
        public void BeginTrans()
        {
            if (this.IsBeginTrans())
            {
                try
                {
                    //��ʼ����ǰ�ȴ�����
                    if ((conn != null) && (conn.State == ConnectionState.Closed))
                    {
                        conn.Open();
                    }
                    //�Ѿ���ʼ�����������ٲ���
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
                    //�Ѿ���ʼ������ſ����ύ
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
                //�Ѿ���ʼ������ſ���ȡ��
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

        #region ׼��Command��������
        /// <summary>
        /// ׼����Command�����ݿ�����,����,����
        /// </summary>
        /// <param name="command">�����Command</param>
        /// <param name="Sql">SQL���</param>
        /// <param name="sqlType">CommandType����</param>
        /// <param name="sqlParams">�������Ƽ���</param>
        /// <param name="paramValues">����ֵ����</param>
        private void PrepareCommand(SqlCommand command, string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues)
        {
            //�ȴ�����
            if (this.conn.State != ConnectionState.Open)
            {
                this.conn.Open();
            }
            //�Ѵ����ݿ����Ӹ���cmd
            command.Connection = conn;

            //�ж��Ƿ�֧������
            if (IsInTransaction)
            {
                command.Transaction = this.trans;
            }

            //�жϲ������������ֵ�Ƿ�һһ��Ӧ
            if ((sqlParams != null) && (sqlParams.Length != paramValues.Length))
            {
                throw new ApplicationException("Param Value not Match");
            }

            //��SQL������������Command
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

        #region �����ݿ�����ݽ��в���
        public void ExecuteNonQuery(string Sql)
        {
            //��nullת��������,�����Ѿ�ʵ�ֵķ���
            this.ExecuteNonQuery(Sql, CommandType.Text, new string[0], new object[0]);
        }

        public void ExecuteNonQuery(string Sql, CommandType sqlType, string sqlParam, object paramValue)
        {
            //�Ѳ�������ת��������,�ٵ����Ѿ�ʵ�ֵķ���
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
                //�����µ�Command����
                SqlCommand cmd = new SqlCommand();

                //׼����Command
                this.PrepareCommand(cmd, Sql, sqlType, sqlParams, paramValues);

                //���sql���ִ��ʱ��
                cmd.CommandTimeout = this.TimeOut;

                //ִ���޸����ݿ������ݵ�����
                cmd.ExecuteNonQuery();

                //Ϊ���´�ʹ��Command,������еĲ���
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

                //׼�����
                SqlParameter objParameter = null;
                for (int i = 0; i < ParamNames.Length; i++)
                {
                    objParameter = new SqlParameter();
                    objParameter.ParameterName = ParamNames[i];
                    objParameter.Value = ParamValues[i];
                    cmd.Parameters.Add(objParameter);
                }

                //���sql���ִ��ʱ��
                cmd.CommandTimeout = this.TimeOut;

                //ִ���޸����ݿ������ݵ�����
                cmd.ExecuteNonQuery();

                //Ϊ���´�ʹ��Command,������еĲ���
                cmd.Parameters.Clear();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Execute SQLSERVER Store Procedure Failed! Procedure Name : " + ProcedureName + "  Error : " + ex.Message);
            }
        }
        #endregion

        #region ִ�в�ѯ�������ݼ�
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
            //�Ѳ�������ת��������,�ٵ����Ѿ�ʵ�ֵķ���
            string[] sqlParams = new string[1];
            object[] paramValues = new object[1];
            sqlParams[0] = sqlParam;
            paramValues[0] = paramValue;

            return this.ExecuteDataset(Sql, sqlType, sqlParams, paramValues, tableName);
        }

        public DataSet ExecuteDataset(string Sql, CommandType sqlType, string[] sqlParams, object[] paramValues, string tableName)
        {
            //������entityData Ϊ��ʱ��,��Ҫ�½�һ��DataSet
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
                //׼����Command
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, Sql, sqlType, sqlParams, paramValues);

                //���sql���ִ��ʱ��
                cmd.CommandTimeout = this.TimeOut;

                //��entityData���������
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;

                //�������ƴ���ʱ��,��Ҫ����ӳ��
                if ((tableName != null) && (tableName != ""))
                {
                    ad.Fill(entityData, tableName);
                }
                else
                {
                    ad.Fill(entityData);
                }

                //Ϊ���´�ʹ��Command,������еĲ���
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
            //�Ѳ�������ת��������,�ٵ����Ѿ�ʵ�ֵķ���
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

        #region ��ѯ���ص���ֵ
        public string ExecuteScalar(string Sql)
        {
            return this.ExecuteScalar(Sql, CommandType.Text, new string[0], new object[0]);
        }

        public string ExecuteScalar(string Sql, CommandType sqlType, string sqlParam, object paramValue)
        {
            //�Ѳ�������ת��������,�ٵ����Ѿ�ʵ�ֵķ���
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
                //׼����Command
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, Sql, sqlType, sqlParams, paramValues);

                //���sql���ִ��ʱ��
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

        #region ��ҳ��ѯ
        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="sqlPhase">sql string</param>
        /// <param name="paramsName">���������б�</param>
        /// <param name="paramsValue">����ֵ�б�</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <param name="rowCount">������,Ҫ�ش�</param>
        /// <returns></returns>
        public DataSet QueryPageFromSql(string strSql, string[] paramsName, object[] paramsValue, int pageIndex, int pageSize, out int rowCount)
        {

            //ȡ������
            rowCount = int.Parse(this.ExecuteDataset(RowCountSql(strSql), CommandType.Text, paramsName, paramsValue).Tables[0].Rows[0][0].ToString());

            strSql = PageSql(strSql, pageIndex, pageSize);

            DataSet dsItems = this.ExecuteDataset(strSql, CommandType.Text, paramsName, paramsValue);

            return dsItems;
        }

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="sqlPhase">sql string</param>        
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <param name="rowCount">������,Ҫ�ش�</param>
        /// <returns></returns>
        public DataSet QueryPageFromSql(string strSql, int pageIndex, int pageSize, out int rowCount)
        {

            //ȡ������
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
