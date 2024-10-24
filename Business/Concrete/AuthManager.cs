using Business.Abstract;
using Business.Validations.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccesResults;
using Core.Utilities.Security.Abstract;
using Entities.Common;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO)
        {
            var findUser=await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            if (findUser == null) 
                 findUser=await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            if (findUser == null)
                return new ErrorDataResult<Token>(message:"User does not exist!",HttpStatusCode.NotFound);

            var result=await _signInManager.CheckPasswordSignInAsync(findUser, loginDTO.Password,false);
            var userRoles=await _userManager.GetRolesAsync(findUser);
            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessToken(findUser, roles: userRoles.ToList());
                return new SuccessDataResult<Token>(data:token,HttpStatusCode.OK);  
            }
            else
            {
                return new ErrorDataResult<Token>(message:"Username or Password is not valid",HttpStatusCode.BadRequest);
            }
            
        }

        //[ValidationAspect]
        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {

            var validator = new RegisterValidation();
            var validationResult=validator.Validate(model);
            if (!validationResult.IsValid)
            {
                //Log.Error(validationResult.ToString());
                return new ErrorResult(message: validationResult.ToString(),HttpStatusCode.BadRequest);
            }

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
