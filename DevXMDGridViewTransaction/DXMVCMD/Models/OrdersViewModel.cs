using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCMD.Models
{
    public class OrdersViewModel
    {
        public IList<OrderViewModel> Source { get; set; }
        public IList<CustomerViewModel> CustomersLookUpData { get; set; }

        public IList<OrderDetailsViewModel> Details { get; set; }

    }
}