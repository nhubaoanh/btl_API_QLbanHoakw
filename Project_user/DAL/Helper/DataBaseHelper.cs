using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public string Strconnection { get; set; } = string.Empty;
        public SqlConnection sqlConnection { get; set; }

        public SqlTransaction sqlTransaction { get; set; }


        public DataBaseHelper(IConfiguration configuration)
        {
            Strconnection = configuration["ConnectionStrings:DefaultConnection"];

        }


        public string OpenConnection()
        {
            try
            {
                if (sqlConnection == null)
                    sqlConnection = new SqlConnection(Strconnection);
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
                return "";

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string OpenConnectionAndBeginTransaction()
        {
            try
            {
                if (sqlConnection == null)
                    sqlConnection = new SqlConnection(Strconnection);
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                return "";


            }
            catch (Exception e)
            {
                if (sqlConnection != null)
                    sqlConnection.Dispose();
                if (sqlTransaction != null)
                    sqlTransaction.Dispose();
                return e.Message;
            }
        }

        public string CloseConnection()
        {

            try
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public DataTable ExcuteSProcedureReturnDataTable(out string mgsError, string sprocedureName, params object[] paramObjects)
        {
            DataTable tb = new DataTable();
            SqlConnection connection;
            try
            {
                SqlCommand cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = sprocedureName };
                connection = new SqlConnection(Strconnection);
                cmd.Connection = connection;

                int parameterInput = (paramObjects.Length) / 2;

                int j = 0;
                for (int i = 0; i < parameterInput; i++)
                {
                    string paramName = Convert.ToString(paramObjects[j++]).Trim();
                    object value = paramObjects[j++];
                    if (paramName.ToLower().Contains("json"))
                    {
                        cmd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = paramName,
                            Value = value ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar
                        });
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                    }
                }

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(tb);
                cmd.Dispose();
                ad.Dispose();
                connection.Dispose();
                mgsError = "";
            }
            catch (Exception exception)
            {
                tb = null;
                mgsError = exception.ToString();
            }
            return tb;
        }

        public object ExcuteScalarSprocedureWithTransaction(out string mgsError, string sprocedure, params object[] paramObjects)
        {
            mgsError = "";
            object result = null;
            using (SqlConnection connection = new SqlConnection(Strconnection))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = sprocedure;
                        cmd.Transaction = transaction;
                        cmd.Connection = connection;

                        int parameterInput = (paramObjects.Length) / 2;
                        int j = 0;
                        for (int i = 0; i < parameterInput; i++)
                        {
                            string paramName = Convert.ToString(paramObjects[j++]);
                            object value = paramObjects[j++];
                            if (paramName.ToLower().Contains("json"))
                            {
                                cmd.Parameters.Add(new SqlParameter()
                                {
                                    ParameterName = paramName,
                                    Value = value ?? DBNull.Value,
                                    SqlDbType = SqlDbType.NVarChar
                                });
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                            }
                        }

                        result = cmd.ExecuteScalar();
                        cmd.Dispose();
                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {

                        result = null;
                        mgsError = exception.ToString();
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex) { }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }


        public List<object> ExecuteScalarSProcedure(out List<string> msgErrors, List<StoreParameterInfo> storeParameterInfos)
        {
            msgErrors = new List<string>();
            bool isSuccess = true;
            List<object> result = new List<object>();
            using (SqlConnection connection = new SqlConnection(Strconnection))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = transaction;
                        cmd.Connection = connection;

                        for (int p = 0; p < storeParameterInfos.Count; p++)
                        {
                            try
                            {
                                cmd.CommandText = storeParameterInfos[p].StoreProcedureName;
                                int parameterInput = storeParameterInfos[p].StoreProcedureParams == null ? 0 : (storeParameterInfos[p].StoreProcedureParams.Count) / 2;
                                int j = 0;

                                if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                                    cmd.Parameters.Clear();

                                for (int i = 0; i < parameterInput; i++)
                                {
                                    string paramName = Convert.ToString(storeParameterInfos[p].StoreProcedureParams[j++]);
                                    object value = storeParameterInfos[p].StoreProcedureParams[j++];
                                    if (paramName.ToLower().Contains("json"))
                                    {
                                        cmd.Parameters.Add(new SqlParameter()
                                        {
                                            ParameterName = paramName,
                                            Value = value ?? DBNull.Value,
                                            SqlDbType = SqlDbType.NVarChar
                                        });
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                                    }
                                }

                                var rresult = cmd.ExecuteScalar();
                                result.Add(rresult);
                            }
                            catch (Exception exception)
                            {
                                isSuccess = false;
                                result.Add(null);
                                msgErrors.Add(exception.StackTrace);
                            }
                        }
                    }
                    if (isSuccess)
                        transaction.Commit();
                    else
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return result;
        }

        public void SetConnectionString(string connectionString)
        {
            Strconnection = connectionString;
        }
    }
}
