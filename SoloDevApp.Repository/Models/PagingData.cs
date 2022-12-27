using System.Collections.Generic;

namespace SoloDevApp.Repository.Models
{
    public class PagingData<T>
    {

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRow { get; set; }
        public string Keywords { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}