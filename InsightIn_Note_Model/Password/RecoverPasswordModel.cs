using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Model.Password
{
    public class RecoverPasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OTP { get; set; }
    }
}
