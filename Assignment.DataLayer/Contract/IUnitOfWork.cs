using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataLayar.Contract
{
    public interface IUnitOfWork
    {

        IVendorService VendorService { get;  }
        IMaterialService MaterialService { get;  }
        IPurchaseService PurchaseService { get;  }
    }
}
