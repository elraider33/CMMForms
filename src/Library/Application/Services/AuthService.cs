using Library.Application.Helpers;
using Library.Application.Models;
using Library.Domain.Options;
using Library.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public interface IAuthService
    {
        AuthModel Authenticate(string username, string password);
        AuthModel AuthenticateWithDocksite(string username, string token);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<Settings> _settings;
        public AuthService(IUserRepository userRepository, IOptions<Settings> settings)
        {
            _userRepository = userRepository;
            _settings = settings;
        }

        public AuthModel Authenticate(string username, string password)
        {
            var authModel = new AuthModel();
            var user = _userRepository.GetByUsername(username);
            if (user == null) {
                authModel.StatusCode = 404;
                authModel.Message = "Username not found";
                authModel.AccessToken = string.Empty;
                return authModel;
            };
            //Password stuff
            var hashed = EncryptionHelper.Sha256Hash(password);
            var isPasswordOK = EncryptionHelper.Verify(hashed, user.Services.Password.Bcrypt);
            if (!isPasswordOK)
            {
                authModel.StatusCode = 401;
                authModel.Message = "Password is wrong";
                authModel.AccessToken = string.Empty;
                return authModel;
            };
            // if pasword's ok continue with the process if it's not throw and exception?
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Value.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(ClaimsHelper.GetClaims(user)),
                Expires = DateTime.UtcNow.AddMinutes(_settings.Value.AccessTokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )

            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            authModel.StatusCode = 200;
            authModel.Message = string.Empty;
            authModel.AccessToken = tokenHanlder.WriteToken(token);;
            return authModel;
        }

        public AuthModel AuthenticateWithDocksite(string username, string tokenStr)
        {
            var authModel = new AuthModel();
            var user = _userRepository.GetByUsername(username);
            if (user == null)
            {
                authModel.StatusCode = 404;
                authModel.Message = "Username not found";
                authModel.AccessToken = string.Empty;
                return authModel;
            };
            //token stuff
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokenStr);
            // if pasword's ok continue with the process if it's not throw and exception?
            if(DateTime.Now > jwtSecurityToken.ValidTo)
            {
                authModel.StatusCode = 401;
                authModel.Message = "Docksite session has expired";
                authModel.AccessToken = string.Empty;
                return authModel;
            }
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Value.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(ClaimsHelper.GetClaims(user)),
                Expires = DateTime.UtcNow.AddMinutes(_settings.Value.AccessTokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )

            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            authModel.StatusCode = 200;
            authModel.Message = string.Empty;
            authModel.AccessToken = tokenHanlder.WriteToken(token); ;
            return authModel;
        }
    }
}
