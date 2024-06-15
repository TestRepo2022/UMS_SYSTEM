using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comman.Autho;
using DataAccessLayer.AuthoDB;
using Microsoft.Data.SqlClient;
using Utility;

namespace DataAccessLayer.AuthoDB
{
    public class AuthDB
    {
        public Tuple<string,DataTable> ValidateUser(UserLogin user)
        {
            DataTable dt = new DataTable();
            string message = string.Empty;
            ArrayList arSqlParameters =new ArrayList();
            try
            {
                SqlParameter sqlParameter;

                sqlParameter = new SqlParameter();
                sqlParameter.Value = user.UserName;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.ParameterName = "@USERNAME";
                sqlParameter.SqlDbType = SqlDbType.VarChar;
                arSqlParameters.Add(sqlParameter);

                sqlParameter = new SqlParameter();
                sqlParameter.Value = user.Password;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.ParameterName = "@PASSWORD";
                sqlParameter.SqlDbType = SqlDbType.VarChar;
                arSqlParameters.Add(sqlParameter);

               dt=ZSConfigDB.GetDataTableSP("LOGINUSER", arSqlParameters);
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            } 
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }

            return new Tuple<string, DataTable>(message, dt);

        }
    }
}
