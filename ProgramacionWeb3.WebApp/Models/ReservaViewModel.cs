using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProgramacionWeb3.Dominio.Entidades;
using ProgramacionWeb3.Dominio.Entidades.Metadata;

namespace ProgramacionWeb3.WebApp.Models
{
    [MetadataType(typeof(ReservaMetadata))]
    public class ReservaViewModel : Reserva, IValidatableObject
    {
        public PaqueteViewModel PaqueteViewModel { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CantPersonas <= 0)
            yield return new ValidationResult("Debe especificar un numero positivo de personas");
        }
    }
}