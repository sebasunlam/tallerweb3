using System.Collections;
using System.Collections.Generic;

namespace ProgramacionWeb3.WebApp.Models
{
    public interface IPagedModel<T>
    {
        List<T> List { get; set; }
        int TotalItems { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        List<int> AvaiableSizes { get;}
    }
}