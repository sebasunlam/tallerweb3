using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Entidades.Metadata;

namespace ProgramacionWeb3.WebApp.Models
{
    [MetadataType(typeof(PaqueteMetadata))]
    public class PaqueteViewModel : Paquete, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaFin.Date < FechaInicio.Date)
                yield return new ValidationResult("La fecha de fin no puede ser menor que la de inicio");
        }
    }
}