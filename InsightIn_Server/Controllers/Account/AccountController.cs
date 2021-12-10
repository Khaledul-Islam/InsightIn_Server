using InsightIn_Note_Model.Password;
using InsightIn_Note_Model.User;
using InsightIn_Note_Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn_Server.Controllers.Account
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult IsExist(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid Email...!!!");
            }
            var response = _service.isExist(email, out string errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(UserAccount userAccount)
        {
            if (userAccount == null)
            {
                return BadRequest("NO User Account");
            }
            var response = _service.Register(userAccount, out string errMsg);
            if (response == false)
            {
                return BadRequest(errMsg);
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok("User Created Successfully");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(SignInModel signInModel)
        {
            if (signInModel == null)
            {
                return BadRequest("Required data for user sign in not found");
            }
            var response = _service.Login(signInModel, out string errMsg);
            if (response == null)
            {
                return Unauthorized("User authentication failed. Please enter valid user account details for sign in.");
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return NotFound("User not found. Please enter valid user name");
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult ChangePassword(PasswordChangeModel model)
        {
            if (model == null)
            {
                return BadRequest("Empty Operation");
            }
            var response = _service.ChangePassword(model, out string errMsg);
            if (response == false)
            {
                return BadRequest(errMsg);
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ForgetPassword(RecoverPasswordModel recover)
        {
            if (recover == null)
            {
                return BadRequest("Empty Operation");
            }
            var response = _service.ForgetPassword(recover, out string errMsg);
            if (response == false)
            {
                return BadRequest(errMsg);
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(new RecoverPasswordModel()
            {
                Email = recover.Email,
                OTP = "Check Your Email :" + recover.Email + " . within 5 minutes to verify."
            });
        }
    }
}
