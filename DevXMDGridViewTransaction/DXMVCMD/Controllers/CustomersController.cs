using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXMVCMD.Models;
using DevExpress;
using DevExpress.Xpo;
using DevExpress.Web.Mvc;

namespace DXMVCMD.Controllers
{
    public class CustomersController : DXBaseXPOController<Customer>
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View(GetCustomers());
        }

        public ActionResult IndexPartial()
        {
            return PartialView("IndexPartial", GetCustomers());
        }
         IEnumerable<CustomerViewModel> GetCustomers()
        {
            return (from c in XpoSession.Query<Customer>().ToList()
                    select new CustomerViewModel() {ID = c.Oid, Name = c.Name}).ToList(); 
                    
        }
        [HttpPost]
        public ActionResult EditCustomer([ModelBinder(typeof(DevExpressEditorsBinder))] CustomerViewModel customer)
         {
             Save(customer);
             return PartialView("IndexPartial", GetCustomers());
         }

        [HttpPost]
        public ActionResult DeleteCustomer([ModelBinder(typeof (DevExpressEditorsBinder))] CustomerViewModel customer)
        {
            Delete(customer);
            return PartialView("IndexPartial", GetCustomers());
        }
         
       

    }
}