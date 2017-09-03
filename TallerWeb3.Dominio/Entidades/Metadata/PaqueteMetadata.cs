using System;
using System.ComponentModel.DataAnnotations;


namespace ProgramacionWeb3.Dominio.Entidades.Metadata
{
    public class PaqueteMetadata
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage = "El campo Fecha Inicio obligatorio")]
        [DataType(DataType.Date,ErrorMessage = "La Fecha de Inicio debe ser una fecha válida")]
        [Display(Name = "Fecha Inicio")]
        public System.DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "El campo Fecha Fin es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "La Fecha de Fin debe ser una fecha válida")]
        [Display(Name = "Fecha Fin")]
        public System.DateTime FechaFin { get; set; }
        public bool Destacado { get; set; }
        [Display(Name = "Lugares Disponibles")]
        public Nullable<int> LugaresDisponibles { get; set; }
        [Required(ErrorMessage = "El campo Precio por Persona es obligatorio")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio por Persona (AR$S)")]
        public int PrecioPorPersona { get; set; }
    }
}