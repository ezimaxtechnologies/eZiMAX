using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System.Reflection;
using System.Configuration;

namespace DXMVCMD
{
    public static class XpoHelper
    {
        private const string ConnectionString = "DefaultConnection";

        static XpoHelper()
        {
            UpdateDatabase();
        }

        public static Session GetNewSession()
        {
            return new Session(DataLayer);
        }

        public static UnitOfWork GetNewUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        //public static NestedUnitOfWork GetNewNestedUnitOfWork()
        //{
        //    return new NestedUnitOfWork(DataLayer);
        //}

        private readonly static object lockObject = new object();

        static volatile IDataLayer fDataLayer;

        static IDataLayer DataLayer
        {
            get 
            { 
            if(fDataLayer==null)
            lock(lockObject)
            {
                if (fDataLayer == null)
                    fDataLayer = GetDataLayer();
            }
            return fDataLayer;
            }
        }

        private static IDataLayer GetDataLayer()
        {
            XpoDefault.Session = null;
            XPDictionary dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(Assembly.GetExecutingAssembly());

            return new ThreadSafeDataLayer(dict, XpoDefault.GetConnectionProvider(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString,AutoCreateOption.None));

        }

        static void UpdateDatabase()
        {
            using (IDataLayer dal = XpoDefault.GetDataLayer(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString,AutoCreateOption.DatabaseAndSchema))
            {
                using(Session session = new Session(dal))
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    session.UpdateSchema(asm);
                    session.CreateObjectTypeRecords(asm);
                }
            }
        }


    }
}