namespace Dominio.Excepciones
{
    public static class ExcepcionesCustom
    {
        public const string Excepcion_MarcaExiste = "La marca ya existe en el sistema";
        public const string Excepcion_MarcaNoExiste = "La marca no existe en el sistema";
        public const string Excepcion_MarcaNoSeCreo = "La marca no se creó en el sistema";
        public const string Excepcion_VehiculoExiste = "El vehículo ya existe en el sistema";
        public const string Excepcion_VehiculoNoExiste = "El vehículo no existe en el sistema";
        public const string Excepcion_VehiculoNoSeCreo = "El vehículo no se creó en el sistema";
        public const string Excepcion_ErrorConsultaUsuarios = "Hubo un problema al consultar el servicio de clientes";
    }
}