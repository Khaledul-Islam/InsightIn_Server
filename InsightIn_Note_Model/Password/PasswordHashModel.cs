using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Model.Password
{
    public class PasswordHashModel
    {
        public string HashText { get; set; }
        public string SaltText { get; set; }
        public int HashLength { get; set; }
        public int HashIteration { get; set; }
    }
}
