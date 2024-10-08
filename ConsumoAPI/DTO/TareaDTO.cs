﻿namespace ConsumoAPI.DTO
{
    public class TareaResponse
    {
        public int TareaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? Prioridad { get; set; }
        public int MiembroEquipoId { get; set; }
        public int ProyectoId { get; set; }
    }
}
