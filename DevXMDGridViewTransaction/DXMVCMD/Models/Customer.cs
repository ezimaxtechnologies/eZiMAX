using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;

namespace DXMVCMD.Models
{
    public class Customer: XPObject
    {
        public Customer(Session session) : base(session) { }

        private string fName;

        public string Name
        {
            get { return fName; }
            set { SetPropertyValue("Name", ref fName, value); }
        }

        [Association("Customer-Orders")]
        public XPCollection<Order> Orders
        {
            get { return GetCollection<Order>("Orders"); }
        }

    }
}