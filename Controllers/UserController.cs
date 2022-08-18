using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore_api_p001.Models;
using netcore_api_p001.Repositories;
using netcore_api_p001.Services;

namespace netcore_api_p001.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            CultureInfo culture = new CultureInfo("pt-BR");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] User model)
        {
            try
            {
                var user = UserRepository.Get(model.Username, model.Password);

                if (user == null) return BadRequest(new { message = "Usuário ou Senha inválidos !" });

                var token = TokenService.GenerateToken(user);

                user.Password = "";

                return new { user = user, token = token };
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonymous()
        {
            return "Acesso Anônimo";
        }

        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Authenticated()
        {
            return String.Format("Usuário Autenticado: {0}", User.Identity.Name);
        }

        [HttpGet]
        [Route("funcionario")]
        [Authorize(Roles = "employee,manager")]
        public string Employee()
        {
            return "Gerente ou Funcionário";
        }

        [HttpGet]
        [Route("gerente")]
        [Authorize(Roles = "manager")]
        public string Manager()
        {
            return "Somente Gerente";
        }
    }
}
