using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comman.Autho
{
    public class APIResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Ok { get; set; }
        public dynamic Data { get; set; }

        public string Token { get; set; }
    }
}
