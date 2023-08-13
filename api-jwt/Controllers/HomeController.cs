using api_jwt.Models;
using api_jwt.Repositories;
using api_jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace api_jwt.Controllers
{
    [Route("v1/account")]
    public class HomeController : Controller
    {


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenServices.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token,
            };
        }


        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anon";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);

        [HttpGet]
        [Route("regularuser")]
        [Authorize(Roles = "administrator,regularuser")]
        public string regularUser() => "Regular User";

        [HttpGet]
        [Route("administrator")]
        [Authorize(Roles = "administrator")]
        public string Administrator() => "Admin";
    }
}
