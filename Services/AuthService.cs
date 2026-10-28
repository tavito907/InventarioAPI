using System.Security.Claims;
using InventarioAPI.Data;
using InventarioAPI.DTOs.Usuarios;
using InventarioAPI.Interfaces;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using InventarioAPI.Exceptions;
using InventarioAPI.Enums;

namespace InventarioAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AuthService(AppDbContext context, IConfiguration configuration, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioDto> RegistrarAsync(RegistroUsuarioDto dto)
        {
            await ValidarCorreoDisponibleAsync(dto.Correo);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Rol = RolUsuario.Empleado
            };

            usuario.PasswordHash = _passwordHasher.HashPassword(
                usuario,
                dto.Password);

            await _context.Usuarios.AddAsync(usuario);

            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo);

            if (usuario is null)
                throw new UnauthorizedException("Correo o contraseña incorrectos.");

            var resultado = _passwordHasher.VerifyHashedPassword(
                usuario, usuario.PasswordHash, dto.Password);

            if (resultado != PasswordVerificationResult.Success)
                throw new UnauthorizedException("Correo o contraseña incorrectos.");

            return GenerarToken(usuario);
        }

        private async Task ValidarCorreoDisponibleAsync(string correo)
        {
            bool existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == correo);

            if (existeCorreo)
                throw new BadRequestException("El correo ya está registrado");
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
