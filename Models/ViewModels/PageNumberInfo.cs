using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class PageNumberInfo
    {
        //box to hold all the info!!
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }
        //calculae the number of pages
        //turn into decimal so then you can divide and have the right amount of apges
        //using ceiling to round up correctly
        public int NumPages =>(int) (Math.Ceiling((decimal) TotalNumItems/ NumItemsPerPage));
    }
}
