using InventarioAPI.Enums;

namespace InventarioAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash {  get; set; } = string.Empty;
        public RolUsuario Rol { get; set; } = RolUsuario.Empleado;
    }
}
