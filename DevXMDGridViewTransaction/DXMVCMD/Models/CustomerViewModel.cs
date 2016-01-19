using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCMD.Models
{
    public class CustomerViewModel : BaseViewModel<Customer>
    {
        public string Name { get; set; }
        public override void GetData(Customer model)
        {
            model.Name = Name;
        }


    }
}