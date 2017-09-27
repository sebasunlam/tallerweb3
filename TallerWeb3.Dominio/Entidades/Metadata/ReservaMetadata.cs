using System.ComponentModel.DataAnnotations;


namespace ProgramacionWeb3.Dominio.Entidades.Metadata
{
    public class ReservaMetadata
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Cantidad de personas")]
        public int CantPersonas { get; set; }
    }
}