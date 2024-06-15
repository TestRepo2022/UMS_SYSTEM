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
    public class ZSConfigDB : DbCall
    {
      
        public static DataSet GetDataSetSP(string spName)
        {
            return new DbCall().GetDataSetSP(spName);
        }
        public static DataSet GetDataSetSP(string spName, ArrayList arSqlParameters)
        {
            return new DbCall().GetDataSetSP(spName, arSqlParameters);
        }
        public static DataTable GetDataTable(string queryString)
        {
            return new DbCall().GetDataTable(queryString);
        }
        public static DataTable GetDataTableSP(string spName)
        {
            return new DbCall().GetDataTableSP(spName);
        }
        public static DataTable GetDataTableSP(string spName, ArrayList arSqlParameters)
        {
            return new DbCall().GetDataTableSP(spName, arSqlParameters);
        }
        public static SqlDataReader GetDataReader(string queryString)
        {
            return new DbCall().GetDataReader(queryString);
        }
        public static SqlDataReader GetDataReaderSP(string spName)
        {
            return new DbCall().GetDataReaderSP(spName);
        }
        public static SqlDataReader GetDataReaderSP(string spName, ArrayList arSqlParameters)
        {
            return new DbCall().GetDataReaderSP(spName, arSqlParameters);
        }
        public static string ExecuteScalar(string queryString)
        {
            return new DbCall().ExecuteScalar(queryString);
        }
        public static string ExecuteScalarSP(string spName)
        {
            return new DbCall().ExecuteScalarSP(spName);
        }
        public static string ExecuteScalarSP(string spName, ArrayList arSqlParameters)
        {
         
            try
            {
                string dbType = string.Empty;
                foreach (var item in arSqlParameters)
                {

                    dbType = ((SqlParameter)item).SqlDbType.ToString();
                    if (dbType.Equals("VarChar") && ((SqlParameter)item).SqlValue != null && !Convert.ToString(((SqlParameter)item).SqlValue).ToUpper().Equals("NULL"))
                    {
                        ((SqlParameter)item).SqlValue = System.Net.WebUtility.HtmlEncode(((SqlParameter)item).SqlValue.ToString()).Replace("&amp;", "&");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            return new DbCall().ExecuteScalarSP(spName, arSqlParameters);
        }
        public static string ExecuteScalarSP(string spName, ArrayList arSqlParameters, SqlTransaction tran)
        {
         
            try
            {
                string dbType = string.Empty;
                foreach (var item in arSqlParameters)
                {

                    dbType = ((SqlParameter)item).SqlDbType.ToString();
                    if (dbType.Equals("VarChar") && ((SqlParameter)item).SqlValue != null && !Convert.ToString(((SqlParameter)item).SqlValue).ToUpper().Equals("NULL"))
                    {
                        ((SqlParameter)item).SqlValue = System.Net.WebUtility.HtmlEncode(((SqlParameter)item).SqlValue.ToString()).Replace("&amp;", "&");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            return new DbCall().ExecuteScalarSP(spName, arSqlParameters, tran);
        }
        public static int ExecuteNonQry(string queryString)
        {
            return new DbCall().ExecuteNonQry(queryString);
        }
        public static int ExecuteNonQry(string queryString, SqlTransaction tran)
        {
            return new DbCall().ExecuteNonQry(queryString, tran);
        }
        public static int ExecuteNonQrySP(string spName)
        {
            return new DbCall().ExecuteNonQrySP(spName);
        }
        public static int ExecuteNonQrySP(string spName, SqlTransaction tran)
        {
            return new DbCall().ExecuteNonQrySP(spName, tran);
        }
        public static int ExecuteNonQrySP(string spName, ArrayList arSqlParameters)
        {
         
            try
            {
                string dbType = string.Empty;
                foreach (var item in arSqlParameters)
                {

                    dbType = ((SqlParameter)item).SqlDbType.ToString();
                    if (dbType.Equals("VarChar") && ((SqlParameter)item).SqlValue != null && !Convert.ToString(((SqlParameter)item).SqlValue).ToUpper().Equals("NULL"))
                    {
                        ((SqlParameter)item).SqlValue = System.Net.WebUtility.HtmlEncode(((SqlParameter)item).SqlValue.ToString()).Replace("&amp;", "&");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            return new DbCall().ExecuteNonQrySP(spName, arSqlParameters);
        }
        public static int ExecuteNonQrySP(string spName, ArrayList arSqlParameters, SqlTransaction tran)
        {
         
            try
            {
                string dbType = string.Empty;
                foreach (var item in arSqlParameters)
                {

                    dbType = ((SqlParameter)item).SqlDbType.ToString();
                    if (dbType.Equals("VarChar") && ((SqlParameter)item).SqlValue != null && !Convert.ToString(((SqlParameter)item).SqlValue).ToUpper().Equals("NULL"))
                    {
                        ((SqlParameter)item).SqlValue = System.Net.WebUtility.HtmlEncode(((SqlParameter)item).SqlValue.ToString()).Replace("&amp;", "&");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                throw ex;
            }
            return new DbCall().ExecuteNonQrySP(spName, arSqlParameters, tran);
        }
    }

}
