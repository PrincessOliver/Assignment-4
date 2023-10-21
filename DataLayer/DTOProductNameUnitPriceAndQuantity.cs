using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DTOProductNameUnitPriceAndQuantity
    {
        public Product? Product{ get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
