using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignmnet.EntityLayer.Models;

namespace Assignment.DataLayar.Contract
{
    public interface IVendorService
    {
        public Task<int> AddVendor(Vendor data);
        public Task<List<Vendor>> GetVendor();
        public Task<Vendor> GetVendorById(int id);
        public Task<int> EditVendor(Vendor data);
        public Task<int> DeleteVendorById(int id);
        public Task<List<Vendor>> GetActiveVendor();
    }
}
