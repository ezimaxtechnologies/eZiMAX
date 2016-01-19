using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;

namespace DXMVCMD.Models
{
    public class Order : XPObject
    {
        public Order(Session session) : base(session) { }
        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue("Name", ref fName, value); }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { SetPropertyValue("OrderDate", ref orderDate, value); }
        }

        private decimal orderTotal;
        public decimal OrderTotal
        {
            get { return orderTotal; }
            set { SetPropertyValue("OrderTotal", ref orderTotal, value); }
        }

        private Customer fCustomer;
        [Association("Customer-Orders"),Aggregated]
        public Customer Customer
        {
            get { return fCustomer; }
            set { SetPropertyValue("Customer", ref fCustomer, value); }
        }

        private XPCollection<OrderDetails> orderDetails;

        [Association("Order-Details")]
        public XPCollection<OrderDetails> OrderDetails
        {
            get { return GetCollection<OrderDetails>("OrderDetails"); }
            //get { orderDetails = GetCollection<OrderDetails>("OrderDetails"); return orderDetails; }
            //set { SetPropertyValue("OrderDetails", value); }
        }

    }
}