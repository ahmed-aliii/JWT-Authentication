using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIAssginment.Models;

namespace WebAPIAssginment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
              if(ModelState.IsValid)
            {
                //Create User
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email
                };

                 IdentityResult result = await userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded) //if creation succeeded
                {
                    // Here you would typically save the user to the database
                    return Ok("User registered successfully");
                }
                else //if creation failed
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("RegistrationError", error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }






        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                // Find the user by username
                ApplicationUser user = await userManager.FindByNameAsync(loginDTO.UserName);
                if (user != null) // Check if the user exists
                {
                    // Check the password
                    bool IsFound = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (IsFound)
                    {
                        //Design token

                        // User Claims
                        List<Claim> claimList = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()), //New JWT ID For Every Login
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                            //new Claim(ClaimTypes.Role, "Admin"),
                        };


                        //Key And Algorithm Type
                        //For Verfication And Trust the Token
                        SymmetricSecurityKey symmetricSecurityKey = 
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        SigningCredentials signingCredentials = 
                            new SigningCredentials(symmetricSecurityKey , SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: configuration["JWT:IssuerIP"],
                            audience: configuration["JWT:AudienceIP"],
                            claims: claimList,
                            expires: DateTime.Now.AddMinutes(30), //Expiration Time
                            signingCredentials: signingCredentials
                            );


                        //Generate Token
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = myToken.ValidTo, //Expiration Time
                        });
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "UserName Or Password in Invalid");
                }
            }
            return BadRequest(ModelState);
        }



    }
}
