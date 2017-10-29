using System.Collections.Generic;

namespace ProgramacionWeb3.WebApp.Models
{
    public class ListadoReservasViewModel : IPagedModel<ReservaViewModel>
    {
        public List<ReservaViewModel> List { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<int> AvaiableSizes => new List<int> { 5, 10, 25, 50, 100 };
    }
}