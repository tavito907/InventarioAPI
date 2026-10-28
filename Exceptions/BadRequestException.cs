namespace InventarioAPI.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string mensaje)
            : base(mensaje, StatusCodes.Status400BadRequest, "BadRequest")
        {

        }
    }
}
