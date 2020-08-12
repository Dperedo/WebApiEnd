using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi02.Helpers;
//
using WebApi02.Model;

namespace WebApi02.Repository
{
    
    public interface IUserService
    {
        string Authenticate(UsuarioDto usermodel);
        IEnumerable<Usuario> GetAll();
        Usuario GetByUserName(string username);
        Usuario Create(Usuario user, string password);
        void Update(Usuario user, string password = null);
        void Delete(string username);
        Usuario GetById(int id);
        
       
        
    }

    public class UserService : IUserService
    {
        private ContenerContext context;
        private readonly AppSettings _appSettings;

        public UserService(ContenerContext contexto, IOptions<AppSettings> appSettings)
        {
            context = contexto;
            _appSettings = appSettings.Value;
        }

        public string Authenticate(UsuarioDto usermodel)
        {
            if (string.IsNullOrEmpty(usermodel.Username) || string.IsNullOrEmpty(usermodel.Password)) return null;

            var user = context.Usuarios.SingleOrDefault(x => x.Username == usermodel.Username);

            if (user == null) return null;

            if (!VerifyPasswordHash(usermodel.Password, user.PasswordHash, user.PasswordSalt)) return null;

            var token = generateJwtToken(user);

            return token;
        }

        public Usuario Create(Usuario user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password es requeriso", "password");
            if (context.Usuarios.Any(x => x.Username == user.Username))
                throw new ArgumentException("Username - " + user.Username + " ya existe");
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Usuarios.Add(user);
            context.SaveChanges();

            return user;

        }

        public void Delete(string username)
        {
            var user = context.Usuarios.Find(username);
            if(user != null)
            {
                context.Usuarios.Remove(user);
                context.SaveChanges();
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            return context.Usuarios;
        }

        public Usuario GetByUserName(string username)
        {
            return context.Usuarios.FirstOrDefault(x => x.Username == username);
        }

        public void Update(Usuario userParam, string password = null)
        {
            var user = context.Usuarios.Find(userParam.Username);

            if (user == null) throw new ArgumentException("Ususario no existe");

            

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            context.Usuarios.Update(user);
            context.SaveChanges();
        }


        //privado

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("no puede ser vacio");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("no puede ser vacio");
            if (storedHash.Length != 64) throw new ArgumentException("");
            if (storedSalt.Length != 128) throw new ArgumentException("");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }

            }
            return true;
        }

        

        public Usuario GetById(int id)
        {
            return context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(Usuario user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
