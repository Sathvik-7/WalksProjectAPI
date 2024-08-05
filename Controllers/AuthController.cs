using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalksProjectAPI.Models.DTO;
using WalksProjectAPI.Repositories;

namespace WalksProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;


        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenReposiory)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenReposiory;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered Successfully, please login..!");
                    }
                }
            }
            return BadRequest("Something went wrong..!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (user != null) 
            {
                var checkPassword = await userManager.CheckPasswordAsync(user,loginRequestDTO.Password);
                
                if (checkPassword) 
                {
                    //Token generation
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        tokenRepository.CreateJWTToken(user, roles.ToList());

                        return Ok();
                    }
                }
            }

            return BadRequest("Username/Password mismatch");
        }
    }
}
