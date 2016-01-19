using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCMD.Models
{
    public class OrderViewModel : BaseViewModel<Order>
    {
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal OrderTotal { get; set; }

        public int Customer { get; set; }

        public XPCollection<OrderDetails> OrderDetails { get; set; }

        public override void GetData(Order model)
        {
            model.Name = Name;
            model.OrderDate = OrderDate;
            model.OrderTotal = OrderTotal;
           // model.OrderDetails = OrderDetails;
            model.Customer = model.Session.GetObjectByKey<Customer>(Customer);
        }

    }
}