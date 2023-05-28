namespace SistemaDeVenta.API.Utilities
{
    public class GenericReponse<T>
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public T Data { get; set; }

    }
}
