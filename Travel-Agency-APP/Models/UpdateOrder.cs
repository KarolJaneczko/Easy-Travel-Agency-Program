using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Agency_APP.Models {
    public class UpdateOrder {
        public int OrdersId { get; set; }
        public string OrdersCountry { get; set; } = null!;
        public string OrdersCity { get; set; } = null!;
        public int OrdersLengthDays { get; set; }
        public string OrdersDescription { get; set; }
    }
}
