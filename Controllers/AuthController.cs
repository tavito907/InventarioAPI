using Microsoft.AspNetCore.Mvc;
using InventarioAPI.Interfaces;
using InventarioAPI.DTOs;
using InventarioAPI.DTOs.Usuarios;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar(RegistroUsuarioDto dto)
        {

             var usuario = await _service.RegistrarAsync(dto);

             return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {

             var token = await _service.LoginAsync(dto);

             return Ok(new
             {
                 Token = token,
             });
        }
    }
}
