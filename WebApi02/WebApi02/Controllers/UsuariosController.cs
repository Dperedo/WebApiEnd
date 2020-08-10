using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.IdentityModel.Tokens;
using WebApi02.Model;
using WebApi02.Repository;
using Microsoft.Extensions.Logging;

namespace WebApi02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuariosController : ControllerBase
    {
        
        private IUserService userService;

        //public UsuariosController(IRepository<Ususario> _repository)
        //{
        //    repository = _repository;
        //}
        private IConfiguration config;
        protected ContenerContext context;
        private ILogger<Usuario> logger;

        public UsuariosController(ContenerContext _context,IUserService _userService, IConfiguration _config, ILogger<Usuario> _loggers)
        {
            this.logger = _loggers;
            this.config = _config;
            this.context = _context;
            this.userService = _userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ObtenerTodos()
        {
            return Ok(userService.GetAll());
        }

        [HttpGet("{name}")]
        public IActionResult ObtenerPorNombre(string name)
        {
            return Ok(userService.GetByUserName(name));
        }

        [HttpDelete("{name}")]
        public void Eliminar(string name)
        {
            logger.LogInformation("Eliminado");
            userService.Delete(name);
        }

        [HttpPut("{name}")]
        public void Actualizar(string name)
        {
            logger.LogInformation("Actualizado");
            var user = userService.GetByUserName(name);
            userService.Update(user);
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UsuarioDto login)
        {
            IActionResult response = Unauthorized();
            
            var user = userService.Authenticate(login.Username, login.Password);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }


            return response;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]UsuarioDto userDto)
        {
            logger.LogInformation("Comienza Registro");
            Usuario user = new Usuario();
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            try
            {
                userService.Create(user, userDto.Password);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Se crea el token utilizando la clase JwtSecurityToken
            //Se le pasa algunos datos como el editor (issuer), audiencia
            // tiempo de expiración y la firma.

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            //Finalmente el método JwtSecurityTokenHandler genera el JWT. 
            //Este método espera un objeto de la clase JwtSecurityToken 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private Usuario AuthenticateUser(Usuario login)
        //{
        //    Usuario user = null;

        //    //Validate the User Credentials  
        //    //Demo Purpose, I have Passed HardCoded User Information  
        //    if (login.Username == "Daniel")
        //    {
        //        //user = new Usuario { username = "Daniel", password = "123456" };  
        //        user = new Usuario { username = login.username, password = login.password, email = login.email };
        //    }
        //    return user;
        //}

    }
}
