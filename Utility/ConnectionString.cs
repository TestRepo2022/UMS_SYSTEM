namespace Utility
{
    public class ConnectionString
    {
        private static string CName = "Data Source=DESKTOP-A9J9S5H;Initial Catalog=Project_db;Integrated Security=True;Pooling=False;TrustServerCertificate=True";

        public static string GetConnection()
        {
            return CName;
        }
    }
}