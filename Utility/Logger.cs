using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Logger
    {
        public static void WriteLog(Exception ex)
            {
                string path = @"C:\\Log";
                string filename = "Log_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
                string fullpath = Path.Combine(path, filename);
                FileStream stream = new FileStream(fullpath, FileMode.Append);
                StreamWriter writer = new StreamWriter(stream);
                string message = "========================Log infromation====================" + Environment.NewLine;
                message += "Log Date Time: " + DateTime.Now + Environment.NewLine;
                message += "Message: " + ex.Message + Environment.NewLine;
                message += "Inner Message : " + ex.InnerException;
                message += "Stack Message : " + ex.StackTrace;

                writer.WriteLine(message);
                writer.Close(); stream.Close();
            }
        } 
}
