namespace Dominio.Interfaces.Servicios
{
    public interface IHttpConfiguration
    {
        HttpClient HttpClientFactory(string NombreConexion);
    }
}