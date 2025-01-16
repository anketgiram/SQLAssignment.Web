using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DataLayar.Contract;

using Assignmnet.EntityLayer.Models;

namespace Assignment.DataLayar.Implementation
{
    public class UnitofWork:IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IVendorService VendorService { get; private set; }
        public IMaterialService MaterialService { get; private set; }
        public IPurchaseService PurchaseService { get; private set; }
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            VendorService = new VendorService(_context);
            MaterialService = new MaterialService(_context);
            PurchaseService = new PurchaseService(_context);
        }

       
    }
}
