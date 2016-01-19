using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Exceptions;
using DXMVCMD.Models;

namespace DXMVCMD.Controllers
{
    public abstract class DXBaseXPOController<T> : Controller where T:XPObject {

        UnitOfWork fSession;
    
        //// GET: DXBase
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public DXBaseXPOController() : base()
        {
            fSession = CreateSession();
        }

        protected UnitOfWork XpoSession
        {
            get { return fSession; } 
        }

        protected virtual UnitOfWork CreateSession()
        {
            return XpoHelper.GetNewUnitOfWork();
        }

        bool Save(BaseViewModel<T> viewModel, bool delete)
        {
            T model = XpoSession.GetObjectByKey<T>(viewModel.ID);
            if(model==null && !delete)            
                model = (T)XpoSession.GetClassInfo<T>().CreateNewObject(XpoSession);
            if (!delete)
                viewModel.GetData(model);
            else if (model != null)
                XpoSession.Delete(model);
            try
            {
                XpoSession.CommitChanges();
                return true;
            }
            catch(LockingException)
            { return false; }
            
        }

        protected bool Save(BaseViewModel<T> viewModel)
        {
            return Save(viewModel, false);
        }

        protected bool Delete(BaseViewModel<T> viewModel)
        {
            return Save(viewModel, true);
        }


    }
}