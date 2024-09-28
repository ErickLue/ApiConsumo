namespace ConsumoAPI.DTO
{
    public class ProyectoResponse
    {
        public int ProyectoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? Presupuesto { get; set; }
        public int UsuarioId { get; set; }
        //public virtual UsuarioResponse Usuario { get; set; } = null!;
    }
}
