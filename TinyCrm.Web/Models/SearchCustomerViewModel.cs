using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCrm.Web.Models
{
    public class SearchCustomerViewModel
        {
        public Core.Model.Options.SearchCustomerOptions SearchOptions { get; set; }
        public string ErrorText { get; set; }
    }
}
