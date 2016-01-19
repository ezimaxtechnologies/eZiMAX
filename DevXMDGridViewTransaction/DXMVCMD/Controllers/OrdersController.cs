using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXMVCMD.Models;
using DevExpress.Xpo;
using DevExpress.Web.Mvc;



namespace DXMVCMD.Controllers
{
    public class OrdersController : DXBaseXPOController<Order>
    {
        // GET: Orders
        public ActionResult Index()
        {
            return View(GetOrders());
        }

        OrdersViewModel GetOrders(int OrderID = 0)
        {
            return new OrdersViewModel()
            {
                Source = (from o in XpoSession.Query<Order>().ToList()
                          select new OrderViewModel()
                          {
                              ID = o.Oid,
                              Name = o.Name,
                              OrderDate = o.OrderDate.Date,
                              OrderTotal = (from d in XpoSession.Query<OrderDetails>().ToList()
                                            where d.Order.Oid == o.Oid
                                            select new OrderDetailsViewModel()
                                                     {
                                                         ID = d.Oid,
                                                         ProductID = d.ProductID,
                                                         Price = d.Price,
                                                         Qty = d.Qty,
                                                         Order = d.Order.Oid


                                                     }
                                                ).ToList().Sum(k => k.Total)
                              // Customer = o.Customer.Oid
                          }
                              ).ToList(),
                CustomersLookUpData = (from c in XpoSession.Query<Customer>().ToList()
                                       select new CustomerViewModel()
                                       {
                                           ID = c.Oid,
                                           Name = c.Name
                                       }
                                           ).ToList(),
                Details = (from d in XpoSession.Query<OrderDetails>().ToList()
                           where d.Order.Oid == OrderID
                           select new OrderDetailsViewModel()
                           {
                               ID = d.Oid,
                               ProductID = d.ProductID,
                               Price = d.Price,
                               Qty = d.Qty,
                               Order = d.Order.Oid

                           }
                               ).ToList()
            };
        }

        [ValidateInput(false)]
        public ActionResult IndexPartial()
        {

            return PartialView("_IndexPartial", GetOrders());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IndexPartialAddNew(DXMVCMD.Models.OrderViewModel item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Save(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = GetOrders();
            return PartialView("_IndexPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexPartialUpdate(DXMVCMD.Models.OrderViewModel item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Save(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = GetOrders();
            return PartialView("_IndexPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IndexPartialDelete(System.Int32 ID)
        {

            if (ID >= 0)
            {
                try
                {
                    //Delete(id)
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = GetOrders();
            return PartialView("_IndexPartial", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewOrderDetail(int OrderID=0)
        {
            TempData["OrderID"] = OrderID;
            return PartialView("_GridViewOrderDetail", GetOrders(OrderID).Details);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewOrderDetailAddNew(DXMVCMD.Models.OrderDetailsViewModel item, int OrderID=0)
        {
            TempData["OrderID"] = OrderID;
            var model = GetOrders(OrderID);
            if (ModelState.IsValid)
            {
                XpoSession.BeginTransaction();
                try
                {

                


                    OrderDetails od = new OrderDetails(XpoSession);
                    od.ProductID = item.ProductID;
                    od.Price = item.Price;
                    od.Qty = item.Qty;
                    od.Total = item.Total;

                    OrderViewModel vm = new OrderViewModel();
                    vm = model.Source.Where(o => o.ID == OrderID).SingleOrDefault();
                    vm.OrderDetails.Add(od);
                    Save(vm);
                    XpoSession.CommitTransaction();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    XpoSession.RollbackTransaction();
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewOrderDetail", model.Details);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewOrderDetailUpdate(DXMVCMD.Models.OrderDetailsViewModel item, int OrderID)
        {
            TempData["OrderID"] = OrderID;
            var model = GetOrders(OrderID);
            if (ModelState.IsValid)
            {
                  XpoSession.BeginTransaction();
                try
                {

                 


                   OrderDetails od = new OrderDetails(XpoSession);
                    od.ProductID= item.ProductID;
                    od.Price  = item.Price;
                    od.Qty = item.Qty;
                    od.Total = item.Total;

                    OrderViewModel vm = new OrderViewModel();
                    vm = model.Source.Where(o => o.ID == OrderID).SingleOrDefault();
                    vm.OrderDetails.Add(od);
                    Save(vm);
                    XpoSession.CommitTransaction();
                }
                catch (Exception e)
                {
                    
                    ViewData["EditError"] = e.Message;
                    XpoSession.RollbackTransaction();
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewOrderDetail", model.Details);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewOrderDetailDelete(System.Int32 ID)
        {
            var model = new object[0];
            if (ID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
           // ViewData["OrderID"] = OrderID;
            return PartialView("_GridViewOrderDetail", GetOrders().Details);
        }
    }
}