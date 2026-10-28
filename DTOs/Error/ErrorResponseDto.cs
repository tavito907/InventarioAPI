namespace InventarioAPI.DTOs.Error
{
    public class ErrorResponseDto
    {
        public int Status { get; set; }
        public string Error { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
