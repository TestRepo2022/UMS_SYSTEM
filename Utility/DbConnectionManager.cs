using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DbConnectionManager
    {
        private SqlConnection _connection;
        public DbConnectionManager()
        {
            _connection= new SqlConnection(ConnectionString.GetConnection());
            if (ConnectionState.Open != _connection.State)
            {
                _connection.Open();
            }
        }

        public SqlConnection getConnection() { return _connection; }
        public void openConnection() {

            try
            {
                if(ConnectionState.Open != _connection.State) {
                    _connection.Open();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        
        }
        public void closeConnection() {

            try
            {
                if (ConnectionState.Open == _connection.State)
                {
                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }


    }
}
