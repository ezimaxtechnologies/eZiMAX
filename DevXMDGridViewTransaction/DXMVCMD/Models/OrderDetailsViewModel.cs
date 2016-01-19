using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCMD.Models
{
    public class OrderDetailsViewModel : BaseViewModel<OrderDetails>
    {
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }
        public decimal Total { get { return Qty * Price;} }

        public override void GetData(OrderDetails model)
        {
            model.ProductID = ProductID;
            model.Qty = Qty;
            model.Price = Price;
            model.Total = Qty * Price;
            model.Order = model.Session.GetObjectByKey<Order>(Order);
        }
    }
}