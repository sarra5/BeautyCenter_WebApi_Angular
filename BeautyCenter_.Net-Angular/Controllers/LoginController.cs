using BeautyCenter_.Net_Angular.Models;
using BeautyCenter_.Net_Angular.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeautyCenter_.Net_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        BeautyCenterContext db;

        public LoginController(BeautyCenterContext context)
        {
            this.db = context;
        }
        [HttpGet]
        public IActionResult login(string Email, string pass)
        {
            var us =db.Set<Userr>().First(s => s.Email == Email && s.Password == pass);

            if(us != null)
            {
                #region claims   

                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim("name", us.Name));
                userdata.Add(new Claim("id", us.Id.ToString()));

                #endregion
                #region secret key
                string key = "welcome to my secret key BeautyCenter Alex";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                #endregion

                var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                #region generate token 
                //header =>hashing algo
                //payload=>claims,expiredate
                //signture => secertkey
                var token = new JwtSecurityToken(
                    claims: userdata,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingcer
                    );

                //token object => encoded string
                //var tokenobjhand = new JwtSecurityTokenHandler();
                //var finaltoken= tokenobjhand.WriteToken(token);
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                #endregion

                return Ok(tokenstring);

            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
