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
    public class LoginController : ControllerBase
    {
        private IUserService userService;

        //public UsuariosController(IRepository<Ususario> _repository)
        //{
        //    repository = _repository;
        //}
        private IConfiguration config;
        protected ContenerContext context;

        public LoginController(ContenerContext _context, IUserService _userService, IConfiguration _config)
        {
            this.config = _config;
            this.context = _context;
            this.userService = _userService;
        }

        [HttpPost("entrar")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Usuario o password es incorrecto" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userService.GetTodos();
            return Ok(users);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] User userDto)
        {
            User user = new User();
            user.Username = userDto.Username;
            user.Password = userDto.Password;
            try
            {
                userService.Register(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
