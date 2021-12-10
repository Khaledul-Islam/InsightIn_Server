using InsightIn_Note_Model.Password;
using InsightIn_Note_Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Contracts
{
    public interface IAccountRepository
    {
        bool Register (UserAccount userAccount,out string errMsg);
        bool UpdateProfile (UserAccount userAccount,out string errMsg);
        UserAccount Login(SignInModel signInModel, out string errMsg);
        bool isExist(string email, out string errMsg);
        bool ChangePassword(PasswordChangeModel model, out string errMsg);
        bool ForgetPassword(RecoverPasswordModel recover, out string errMsg);
    }
}
