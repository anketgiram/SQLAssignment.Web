using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignmnet.EntityLayer.Models;

namespace Assignment.DataLayar.Contract
{
    public interface IPurchaseService
    {
        public Task<List<Purchaseorder>> GetPurchaseOrder();

        public Task<int> AddPurchaseOrder(PurchaseOrderViewModel data);

        public Task<Purchaseorder> GetPurchaseOrderById(int id);
        public Task<List<Purchaseorderdetail>> GetPurchaseOrderDetilas(int id);

        public Task<int> UpdatePurchaseOrder(PurchaseOrderViewModel data);

        public Task<int> DeletePurchaseOrder(int purchaseOrderId);

    }
}
