using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;


namespace DXMVCMD.Models
{
    public class OrderDetails : XPObject
    {
        public OrderDetails(Session session) : base(session) { }

        private int productID;
        public int ProductID
        {
            get { return productID; }
            set { SetPropertyValue("ProductID", ref productID, value); }
        }

        private int qty;

        public int Qty
        {
            get { return qty; }
            set { SetPropertyValue("Qty", ref qty, value); }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { SetPropertyValue("Price", ref price, value); }
        }
        private decimal total;
        public decimal Total
        {
            get { return total; }
            set { SetPropertyValue("Total", ref total, value); }
            
        }

        private Order fOrder;
        [Association("Order-Details"),Aggregated]
        public Order Order
        {
            get { return fOrder; }
            set { SetPropertyValue("Order", ref fOrder, value); }
        }
    }
}