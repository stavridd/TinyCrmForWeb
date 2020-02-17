using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCrm.Web.Models
{
    public class SearchProductViewModel
        {
        public Core.Model.Options.SearchProductOptions SearchOptions { get; set; }
        public string ErrorText { get; set; }
    }
}
