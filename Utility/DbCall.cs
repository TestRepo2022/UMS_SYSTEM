using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DbCall
    {
        private DbConnectionManager dbConnection;
        public DbCall() {
            dbConnection = new DbConnectionManager();
        }

        public DataSet GetDataSetSP(string spName)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection =dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = SqlCmd;
            try
            {
                sqlDA.Fill(dsDataSet);
                SqlCmd.Dispose();
                return dsDataSet;
            }
            catch (Exception ex)
            {
                dsDataSet.Tables.Clear();
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                sqlDA = null;
                SqlCmd = null;
                dsDataSet = null;
               dbConnection.closeConnection();
            }
        }

        public DataSet GetDataSetSP(string spName, ArrayList arSqlParameters)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            SqlCmd.CommandTimeout = 0;
            for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
            {
                SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
            }
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = SqlCmd;
            try
            {
                sqlDA.Fill(dsDataSet);
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }
                SqlCmd.Dispose();
                return dsDataSet;

            }
            catch (Exception ex)
            {
                dsDataSet.Tables.Clear();
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                dbConnection.closeConnection();
                sqlDA = null;
                SqlCmd = null;
                dsDataSet = null;

            }
        }


        public DataTable GetDataTable(string queryString)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = queryString;
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = SqlCmd;
            try
            {
                sqlDA.Fill(dsDataSet);
                SqlCmd.Dispose();
                return dsDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                dsDataSet.Tables.Clear();
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                dbConnection.closeConnection();
                sqlDA = null;
                SqlCmd = null;
                dsDataSet = null;
            }

        }


        public DataTable GetDataTableSP(string spName)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            SqlCmd.CommandTimeout = 0;
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = SqlCmd;
            try
            {
                sqlDA.Fill(dsDataSet);
                SqlCmd.Dispose();
                return dsDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                dsDataSet.Tables.Clear();
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                dbConnection.closeConnection();
                sqlDA = null;
                SqlCmd = null;
                dsDataSet = null;
            }
        }


        public DataTable GetDataTableSP(string spName, ArrayList arSqlParameters)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            SqlCmd.CommandTimeout = 0;
            for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
            {
                SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
            }
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = SqlCmd;
            try
            {
                sqlDA.Fill(dsDataSet);
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }
                SqlCmd.Dispose();
                return dsDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                dsDataSet.Tables.Clear();
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                sqlDA = null;
                SqlCmd = null;
                dsDataSet = null;
            }
        }

        public SqlDataReader GetDataReader(string queryString)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = queryString;
            SqlDataReader sqlDR = null;
            try
            {
                sqlDR = SqlCmd.ExecuteReader();
                SqlCmd.Dispose();
                return sqlDR;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                if (!(sqlDR.IsClosed == true))
                {
                    sqlDR.Close();
                }
                 dbConnection.closeConnection();
                SqlCmd = null;
            }
        }

        public SqlDataReader GetDataReaderSP(string spName)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            SqlDataReader sqlDR = null;
            try
            {
                sqlDR = SqlCmd.ExecuteReader();
                SqlCmd.Dispose();
                return sqlDR;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                if (!(sqlDR.IsClosed == true))
                {
                    sqlDR.Close();
                }
                 dbConnection.closeConnection();
                SqlCmd = null;
            }
        }


        public SqlDataReader GetDataReaderSP(string spName, ArrayList arSqlParameters)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            SqlCmd.CommandTimeout = 0;
            SqlDataReader sqlDR = null;
            for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
            {
                SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
            }
            try
            {
                sqlDR = SqlCmd.ExecuteReader();
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }
                SqlCmd.Dispose();
                return sqlDR;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                if (!(sqlDR.IsClosed == true))
                {
                    sqlDR.Close();
                }
                 dbConnection.closeConnection();
                SqlCmd = null;
            }
        }

        public string ExecuteScalar(string queryString)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = queryString;
            try
            {
                object objVal = SqlCmd.ExecuteScalar();
                return objVal.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
            }

        }


        public string ExecuteScalarSP(string spName)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            try
            {
                object objVal = SqlCmd.ExecuteScalar();
                return objVal.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
            }

        }


        public string ExecuteScalarSP(string spName, ArrayList arSqlParameters, SqlTransaction tran)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = tran.Connection;
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Transaction = tran;
            SqlCmd.CommandText = spName;
            for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
            {
                SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
            }
            try
            {
                object objVal = new object();
                objVal = SqlCmd.ExecuteScalar();
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }

                if (objVal == null)
                {
                    return string.Empty;
                }
                else
                {
                    return objVal.ToString();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
            }
        }


        public string ExecuteScalarSP(string spName, ArrayList arSqlParameters)
        {
            SqlCommand SqlCmd = new SqlCommand();
            object objVal = new object();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = spName;
            for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
            {
                SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
            }

            try
            {
                objVal = SqlCmd.ExecuteScalar();
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }

                if (objVal == null)
                {
                    return string.Empty;
                }
                else
                {
                    return objVal.ToString();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                objVal = null;
            }
        }


        public int ExecuteNonQry(string queryString)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = dbConnection.getConnection();
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandText = queryString;
            SqlCmd.CommandType = CommandType.Text;
            try
            {
                int iRowsAffected = SqlCmd.ExecuteNonQuery();
                return iRowsAffected;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
            }
        }

        public int ExecuteNonQry(string queryString, SqlTransaction tran)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = tran.Connection;
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandText = queryString;
            SqlCmd.Transaction = tran;
            SqlCmd.CommandType = CommandType.Text;
            try
            {
                int iRowsAffected = SqlCmd.ExecuteNonQuery();
                return iRowsAffected;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
            }
        }

        public int ExecuteNonQrySP(string spName)
        {
            SqlCommand SqlCmd = null;
            int iRowsAffected = 0;
            try
            {
                SqlCmd = new SqlCommand();
                SqlCmd.CommandTimeout = 0;
                SqlCmd.Connection = dbConnection.getConnection();
                SqlCmd.CommandText = spName;
                SqlCmd.CommandType = CommandType.StoredProcedure;
                iRowsAffected = SqlCmd.ExecuteNonQuery();
                return iRowsAffected;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
                iRowsAffected = 0;
            }
        }


        public int ExecuteNonQrySP(string spName, SqlTransaction tran)
        {
            SqlCommand SqlCmd = null;
            int iRowsAffected = 0;
            try
            {
                SqlCmd = new SqlCommand();
                SqlCmd.Connection = dbConnection.getConnection();
                SqlCmd.CommandText = spName;
                SqlCmd.Transaction = tran;
                SqlCmd.CommandTimeout = 0;
                SqlCmd.CommandType = CommandType.StoredProcedure;
                iRowsAffected = SqlCmd.ExecuteNonQuery();
                return iRowsAffected;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
                iRowsAffected = 0;
            }
        }


        public int ExecuteNonQrySP(string spName, ArrayList arSqlParameters)
        {

            SqlCommand SqlCmd = null;
            int iRowsAffected = 0;
            try
            {
                SqlCmd = new SqlCommand();
                SqlCmd.Connection = dbConnection.getConnection();
                SqlCmd.CommandTimeout = 0;
                SqlCmd.CommandText = spName;
                SqlCmd.CommandType = CommandType.StoredProcedure;
                for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
                {
                    SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
                }
                iRowsAffected = SqlCmd.ExecuteNonQuery();
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }
                return iRowsAffected;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                 dbConnection.closeConnection();
                SqlCmd = null;
                iRowsAffected = 0;
            }
        }


        public int ExecuteNonQrySP(string spName, ArrayList arSqlParameters, SqlTransaction tran)
        {
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = tran.Connection;
            SqlCmd.CommandTimeout = 0;
            SqlCmd.CommandText = spName;
            SqlCmd.Transaction = tran;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            for (int iIndex = 0; iIndex <= arSqlParameters.Count - 1; iIndex++)
            {
                SqlCmd.Parameters.Add(arSqlParameters[iIndex]);
            }
            try
            {
                int iRowsAffected = SqlCmd.ExecuteNonQuery();
                arSqlParameters.Clear();
                for (int iIndex = 0; iIndex <= SqlCmd.Parameters.Count - 1; iIndex++)
                {
                    if (SqlCmd.Parameters[iIndex].Direction == ParameterDirection.InputOutput | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.Output | SqlCmd.Parameters[iIndex].Direction == ParameterDirection.ReturnValue)
                    {
                        arSqlParameters.Add(SqlCmd.Parameters[iIndex]);
                    }
                }
                return iRowsAffected;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            finally
            {
                // dbConnection.closeConnection()
                SqlCmd = null;
            }
        }
    }
}
