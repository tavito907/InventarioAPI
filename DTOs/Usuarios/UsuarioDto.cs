using InventarioAPI.Enums;

namespace InventarioAPI.DTOs.Usuarios
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public RolUsuario Rol {  get; set; } = RolUsuario.Empleado;
    }
}
