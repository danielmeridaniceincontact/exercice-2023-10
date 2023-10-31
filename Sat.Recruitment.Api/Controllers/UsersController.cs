using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly List<User> _users;
        private readonly IUserService _userService;
        private readonly IErrorService _errorService;

        public UsersController(IUserService userService, IErrorService errorService)
        {
            _users = new List<User>();
            _userService = userService;
            _errorService = errorService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, UserType userType, string money)
        {
            try
            {
                var errors = string.Empty;

                _errorService.ValidateErrors(name, email, address, phone, ref errors);

                if (!string.IsNullOrEmpty(errors))
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = errors
                    };

                var newUser = new User(name, email, address, phone, userType, money);
                newUser.Money = _userService.GetMoneyByUserType(newUser);
                newUser.Email = _userService.NormalizeUserEmail(newUser.Email);
                var isDuplicatedUser = _userService.CheckDuplicatedUser(newUser);

                if (!isDuplicatedUser)
                {
                    Debug.WriteLine("User Created");

                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }
        }        
    }    
}
