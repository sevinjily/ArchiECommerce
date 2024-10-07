using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccesResults;
using Entities.Common;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthManager(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {
            User newUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                return new SuccessResult(System.Net.HttpStatusCode.Created);
            }
            else
            {
                string response = string.Empty;
                foreach (var error in result.Errors)
                {
                    response += error.Description+".";

                }
                return new ErrorResult(response, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
