namespace InventarioAPI.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string mensaje)
            : base(mensaje, StatusCodes.Status404NotFound, "NotFound")
        {
        }
    }
}
