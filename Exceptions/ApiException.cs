namespace InventarioAPI.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }
        public string Error {  get; }

        protected ApiException(string mensaje, int statusCode, string error )
            : base(mensaje)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}
