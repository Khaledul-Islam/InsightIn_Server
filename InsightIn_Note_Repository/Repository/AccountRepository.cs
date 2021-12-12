using InsightIn_Note_DBContext.DBContext_EF;
using InsightIn_Note_Model.Password;
using InsightIn_Note_Model.User;
using InsightIn_Note_Repository.Contracts;
using InsightIn_Note_Utilities.Common;
using InsightIn_Note_Utilities.Helpers;
using InsightIn_Note_Utilities.StaticData;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public AccountRepository(ApplicationDbContext db, IConfiguration configuration, IEmailSender emailSender)
        {
            _db = db;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public bool ChangePassword(PasswordChangeModel model, out string errMsg)
        {
            errMsg = string.Empty;
            //var data = _db.UserAccounts.Where(a => a.UserAccountId == model.UserID && a.UserName == model.UserName);
            var data = _db.UserAccounts.Find(model.UserID);
            if (data == null)
            {
                errMsg = "Not Found";
                return false;
            }
            var passwordDetails = PasswordProtector.GetHashAndSalt(model.Password);
            UserAccount userAccount = new();
            userAccount.Password = passwordDetails.HashText;
            userAccount.PasswordSalt = passwordDetails.SaltText;
            userAccount.HashIteration = passwordDetails.HashIteration;
            userAccount.HashLength = passwordDetails.HashLength;
            userAccount.UpdateDate = DateTime.Now;
            _db.Update(userAccount);
             _db.SaveChanges();
            return true;
        }

        public bool ForgetPassword(RecoverPasswordModel recover, out string errMsg)
        {
            errMsg = string.Empty;
            if (!string.IsNullOrEmpty(recover.OTP))
            {
                var otpObj = _db.OTPServices.OrderByDescending(x => x.ID).FirstOrDefault(x => x.Email.Equals(recover.Email));
                if (otpObj == null)
                {
                    errMsg="Invalid OPT";
                    return false;
                }

                if (otpObj.OTP == recover.OTP)
                {
                    bool verify = false;
                    DateTime otpTime = otpObj.OTPTime;
                    DateTime Now = DateTime.UtcNow;
                    var duration = Now.Subtract(otpTime).TotalSeconds;
                    if (duration <= 300)
                    {
                        verify = true;
                    }
                    else
                    {
                        verify = false;
                    }
                    if (verify)
                    {
                        if (!string.IsNullOrEmpty(recover.Email) && !string.IsNullOrEmpty(recover.NewPassword))
                        {
                            var passwordDetails = PasswordProtector.GetHashAndSalt(recover.NewPassword);
                            UserAccount userAccount = new ();
                            userAccount.Password = passwordDetails.HashText;
                            userAccount.PasswordSalt = passwordDetails.SaltText;
                            userAccount.HashIteration = passwordDetails.HashIteration;
                            userAccount.HashLength = passwordDetails.HashLength;
                            userAccount.UpdateDate = DateTime.Now;
                            _db.UserAccounts.Update(userAccount);
                            _db.OTPServices.RemoveRange(_db.OTPServices.Where(x => x.UserAccountId == otpObj.UserAccountId));
                            _db.SaveChanges();
                            return true;
                        }
                    }
                    else
                    {
                        errMsg = "OTP Time Out";
                        return false;

                    }
                }
            }

            //

            if (string.IsNullOrEmpty(recover.Email))
            {
                errMsg = "Please Provide Valid Email";
                return false;
            }
            else
            {
                if (recover != null && !string.IsNullOrEmpty(recover.Email))
                {
                    var user = _db.UserAccounts.FirstOrDefault(x => x.UserName.Equals(recover.Email));
                    if (user != null && !string.IsNullOrEmpty(user.Password))
                    {
                        string OTP = string.Empty;
                        var bytes = Base32Encoding.ToBytes("JBSWY3DPEHPK3PXP");
                        var totp = new Totp(bytes, step: 300);
                        OTP = totp.ComputeTotp(DateTime.UtcNow);
                        //
                        System.Text.StringBuilder sb = new();
                        sb.Append("<h3>MR. " + user.LastName + ",</h3>");
                        sb.Append("<div>");
                        sb.Append("<p> We have received a request to reset your Note Management password.");
                        sb.Append("To reset your password please use the mentioned six digit code:</p>");
                        sb.Append("<h2>" + OTP + "</h2>");
                        sb.Append("<p>" + "If you did not request a password reset, please ignore this email. OTP will expire in 5 minutes." + "</p><br/>");
                        sb.Append("<p>" + "Thank you," + "</p>");
                        sb.Append("<p>" + "InsightIn Technology Dev Team" + "</p>");
                        var sbbody = sb.ToString();

                        _emailSender.SendEmailAsync(user.Email, "Password Recover Note Management Project", sbbody);
                        OTPService oTPService = new();
                        oTPService.Email = user.Email;
                        oTPService.OTP = OTP;
                        oTPService.UserAccountId = user.UserAccountId;
                        oTPService.OTPTime = DateTime.UtcNow;
                        _db.OTPServices.Add(oTPService);
                        _db.SaveChanges();
                    }
                    else
                    {
                        errMsg = "NO user found";
                        return false;
                    }
                }
                else
                {
                    errMsg = "NO user found";
                    return false;
                }

            }
            return true;
        }

        public bool isExist(string email, out string errMsg)
        {
            errMsg = string.Empty;
            var user = _db.UserAccounts.FirstOrDefault(x => x.Email.Equals(email));
            if (user != null)
            {
                errMsg = "User account already exist...!!";
                return true;
            }
            return false;
        }

        public UserAccount Login(SignInModel signInModel, out string errMsg)
        {
            errMsg = string.Empty;

            if (signInModel != null && !string.IsNullOrEmpty(signInModel.UserName) && !string.IsNullOrEmpty(signInModel.Password))
            {
                var user = _db.UserAccounts.FirstOrDefault(x => x.Email.Equals(signInModel.UserName));
                if (user != null && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.PasswordSalt))
                {
                    bool passwordVerification = PasswordProtector.VerifyPassword
                        (signInModel.Password,
                        user.Password,
                        user.PasswordSalt,
                        user.HashLength
                        , user.HashIteration);

                    if (passwordVerification)
                    {
                        string jwtToken = JwtTokenGenerator.GetJwtToken(user, _configuration);

                        return new UserAccount
                        {
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            DateOfBirth = user.DateOfBirth,
                            Password = "",
                            Role = user.Role,
                            UserAccountId = user.UserAccountId,
                            Token = jwtToken
                        };
                    }
                    else
                    {
                        errMsg = "User authentication failed. Please enter valid user account details for sign in.";
                        return new UserAccount();
                    }
                }
                else
                {
                    errMsg = "User not found. Please enter valid user name";
                    return new UserAccount();
                }

            }
            else
            {
                errMsg = "Required data for user sign in not found";
                return new UserAccount();
            }
        }

        public bool Register(UserAccount userAccount, out string errMsg)
        {
            errMsg = string.Empty;
            var user = _db.UserAccounts.FirstOrDefault(x => x.Email.Equals(userAccount.Email));
            if (user != null)
            {
                errMsg = "User account already exist...!!";
                return false;
            }
            if (userAccount != null && !string.IsNullOrEmpty(userAccount.Email) && !string.IsNullOrEmpty(userAccount.Password))
            {
                var passwordDetails = PasswordProtector.GetHashAndSalt(userAccount.Password);
                userAccount.Password = passwordDetails.HashText;
                userAccount.PasswordSalt = passwordDetails.SaltText;
                userAccount.HashIteration = passwordDetails.HashIteration;
                userAccount.HashLength = passwordDetails.HashLength;
                userAccount.CreatedDate = DateTime.Now;
                userAccount.Version = 1;
                userAccount.UserName = userAccount.Email;
                if (userAccount.Role == null)
                {
                    userAccount.Role = Role.User;
                }

                _db.UserAccounts.Add(userAccount);
                _db.SaveChanges();
            }
            else
            {
                errMsg = "Required data for user registeration not found";
                return false;
            }

            return true;
        }

        public bool UpdateProfile(UserAccount userAccount, out string errMsg)
        {
            throw new NotImplementedException();
        }
    }
}
