using InsightIn_Note_Model.Password;
using InsightIn_Note_Model.User;
using InsightIn_Note_Repository.Contracts;
using InsightIn_Note_Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public bool ChangePassword(PasswordChangeModel model, out string errMsg)
        {
            return _repo.ChangePassword(model, out errMsg);
        }

        public bool ForgetPassword(RecoverPasswordModel recover, out string errMsg)
        {
            return _repo.ForgetPassword(recover, out errMsg);
        }

        public bool isExist(string email, out string errMsg)
        {
            return _repo.isExist(email, out errMsg);
        }

        public UserAccount Login(SignInModel signInModel, out string errMsg)
        {
            return _repo.Login(signInModel, out errMsg);
        }

        public bool Register(UserAccount userAccount, out string errMsg)
        {
            return _repo.Register(userAccount, out errMsg);
        }

        public bool UpdateProfile(UserAccount userAccount, out string errMsg)
        {
            return _repo.UpdateProfile(userAccount, out errMsg);
        }
    }
}
