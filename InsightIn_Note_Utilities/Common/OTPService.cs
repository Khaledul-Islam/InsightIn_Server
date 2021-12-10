using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Utilities.Common
{
    public class OTPService
    {
        [Key]
        public long ID { get; set; }
        public string OTP { get; set; }
        public int UserAccountId { get; set; }
        public string Email { get; set; }
        public DateTime OTPTime { get; set; }
    }
}
