using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignmnet.EntityLayer.Models
{
    public class PurchaseOrderViewModel
    {
        public Purchaseorder Purchaseorder {  get; set; }
        public List<Purchaseorderdetail> Purchaseorderdetail { get; set; }
    }
}
