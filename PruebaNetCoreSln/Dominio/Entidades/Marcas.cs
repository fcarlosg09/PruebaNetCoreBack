namespace Dominio.Entidades
{
    public class Marcas
    {
        #region Propiedades
        public int Id { get; set; }
        public string? Nombre { get; set; }
        #endregion

        #region Relaciones
        public IReadOnlyList<ListadoVehiculos> ListadoVehiculos_Lista { get; set; }
        #endregion
    }
}