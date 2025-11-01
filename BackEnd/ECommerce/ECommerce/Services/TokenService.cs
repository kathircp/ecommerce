using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using ECommerce.Models;

namespace ECommerce.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(JwtSettings settings)
        {
            _settings = settings;
        }

        public string CreateToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_settings.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            //    new Claim(ClaimTypes.Name, user.Username)
            //}
            //.Concat(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)))
            //.ToList();

            //// Ensure key is at least 256 bits (32 bytes). If the provided key is shorter,
            //// derive a 256-bit key by hashing the provided string with SHA-256.
            //byte[] keyBytes = Encoding.UTF8.GetBytes(_settings.Key ?? string.Empty);                        //var key = new SymmetricSecurityKey(keyBytes);            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);            //var token = new JwtSecurityToken(            //    issuer: _settings.Issuer,            //    audience: _settings.Audience,            //    claims: claims,            //    expires: DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),            //    signingCredentials: creds            //);            //return new JwtSecurityTokenHandler().WriteToken(token);        }            }}