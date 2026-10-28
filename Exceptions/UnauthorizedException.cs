namespace InventarioAPI.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string mensaje)
            : base(mensaje, StatusCodes.Status401Unauthorized, "Unauthorized")
        {
        }
    }
}
