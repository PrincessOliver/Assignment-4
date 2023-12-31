﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DTOProductWithCategoryName
    {
        
        public string? CategoryName { get; set; }
        public string? Name { get; set; }
    }

    public class DTOProductNameWithCategoryName
    {
        public string? CategoryName { get; set; }
        public string? ProductName { get; set; }
    }
}
