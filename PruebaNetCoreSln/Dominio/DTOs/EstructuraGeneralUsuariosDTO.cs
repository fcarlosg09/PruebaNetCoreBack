namespace Dominio.DTOs
{
    public class EstructuraGeneralUsuariosDTO
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public IReadOnlyList<UsuariosDTO>? data { get; set; }
    }
}