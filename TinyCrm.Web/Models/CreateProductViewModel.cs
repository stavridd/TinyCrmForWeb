﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCrm.Web.Models
{
    public class CreateProductViewModel
        {
        public Core.Model.Options.AddProductOptions CreateOptions { get; set; }
        public string ErrorText { get; set; }
    }
}
