using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApi_sqlServer.Dtos.Account;
using testApi_sqlServer.Interfaces;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Controllers
{

#pragma warning disable CS8601
#pragma warning disable CS8604 

    [Route("api/account")]
    public class AccountCotroller : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountCotroller(UserManager<AppUser> userManager, ITokenServices tokenServices, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _signinManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.User.ToLower());
            if (user == null) return Unauthorized("Invalid user name!");
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Password Incorrect");
            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenServices.CreateToken(user)
                }
            );
        }

        [HttpPost("reguister")]
        public async Task<IActionResult> Reguister([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var appUser = new AppUser
                {
                    UserName = register.Username,
                    Email = register.Email
                };


                var createUser = await _userManager.CreateAsync(appUser, register.Password);


                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "user");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenServices.CreateToken(appUser)
                            }
                        );
                    }
                    else return StatusCode(500, roleResult.Errors);
                }
                else return StatusCode(500, createUser.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}