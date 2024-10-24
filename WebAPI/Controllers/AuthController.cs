using Business.Abstract;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result=await _authService.RegisterAsync(model);             
            if (result.IsSuccess)
                   return Ok(result);
            return BadRequest(result);
            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result= await _authService.LoginAsync(loginDTO);
            if (result.IsSuccess)
                return Ok(result);
            
                return BadRequest();
        }
    }
}
