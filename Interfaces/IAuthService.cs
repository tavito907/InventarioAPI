using InventarioAPI.Models;
using InventarioAPI.DTOs;
using InventarioAPI.DTOs.Usuarios;

namespace InventarioAPI.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioDto> RegistrarAsync(RegistroUsuarioDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
