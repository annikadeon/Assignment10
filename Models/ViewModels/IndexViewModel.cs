using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class IndexViewModel
    {
        //to hold info for the index page
        public List<Bowlers> Bowlers{get; set;}
        public PageNumberInfo PageNumberInfo { get; set; }
        public string TeamCat { get; set; }
    }
}
