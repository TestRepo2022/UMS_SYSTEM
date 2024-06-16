using Comman.Autho;
using DataAccessLayer.AuthoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BusinessLayer.Autho
{
    public class AuthBL
    {
        AuthDB authDB = new AuthDB();
        public Tuple<string, UserData> ValidateUser(UserLogin user)
        {
            UserData userData = new UserData();
            string message = string.Empty;
            try
            {
                var result = authDB.ValidateUser(user);
                if (!string.IsNullOrEmpty(result.Item1))
                {
                    message = result.Item1;
                    return new Tuple<string, UserData>(message, userData);
                }
                else
                {
                    var dt = result.Item2;
                    userData.Rid = dt.Rows[0]["Rid"].ToString();
                    userData.UserId = dt.Rows[0]["UserId"].ToString();
                    userData.Email = dt.Rows[0]["Email"].ToString();
                    userData.Mobile = dt.Rows[0]["Mobile"].ToString();
                    userData.Gender = dt.Rows[0]["Gender"].ToString();
                    userData.Desgination = dt.Rows[0]["Desgination"].ToString();
                    userData.Department = dt.Rows[0]["Department"].ToString();
                    userData.FirstName = dt.Rows[0]["FirstName"].ToString();
                    userData.LastName = dt.Rows[0]["LastName"].ToString();
                    userData.ProfilePic = dt.Rows[0]["ProfilePic"].ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return new Tuple<string, UserData>(message, userData);
        }
        public Tuple<string, OTPInfo> SenOTP(int Rid)
        {
            OTPInfo userData = new OTPInfo();
            string message = string.Empty;
            try
            {
                var result = authDB.SendOTP(Rid);
                if (!string.IsNullOrEmpty(result.Item1))
                {
                    message = result.Item1;
                    return new Tuple<string, OTPInfo>(message, userData);
                }
                else
                {
                    var dt = result.Item2;
                    userData.Template = dt.Rows[0]["msg"].ToString();
                    userData.Mobile = dt.Rows[0]["mobile"].ToString();
                    //userData.Email = dt.Rows[0]["Email"].ToString();
                    

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return new Tuple<string, OTPInfo>(message, userData);
        }

    }
}
