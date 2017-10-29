using System.Collections.Generic;
using ProgramacionWeb3.Dominio.Entidades;

namespace ProgramacionWeb3.WebApp.Models
{
    public class ListadoPaquetesViewModel : IPagedModel<PaqueteViewModel>
    {
        public List<PaqueteViewModel> List { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<int> AvaiableSizes => new List<int> { 5, 10, 25, 50, 100 };
    }
}