using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.Autho
{
    public class UserData
    {
        public string Rid { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Gender { get; set; }

        public string IsActive { get; set; }

        public string ProfilePic { get; set; }

        public string Department { get; set; }

        public string Desgination { get; set; }
    }
    public class OTPInfo
    {
        public string Template { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

    }
}
