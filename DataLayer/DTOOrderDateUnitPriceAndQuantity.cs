using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DTOOrderDateUnitPriceAndQuantity
    {
        public int OrderId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Order? Order {  get; set; }
    }
}
