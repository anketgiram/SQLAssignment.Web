using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DataLayar.Contract;
using Assignmnet.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.DataLayar.Implementation
{
    public class PurchaseService: IPurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<Purchaseorder>> GetPurchaseOrder()
        {
            try
            {
                var resultdata = await _context.Purchaseorders
                                         .Include(u => u.Vendor).ToListAsync();

                var returnresult =  (from o in resultdata
                                    select new Purchaseorder
                                    {
                                        Id = o.Id,
                                        OrderNo = o.OrderNo,
                                        OrderDate = o.OrderDate,
                                        Notes = o.Notes,
                                        OrderValue = o.OrderValue,
                                        VendorId = o.VendorId,
                                        Vendor = new Vendor
                                        {
                                            Id = o.Vendor.Id,
                                            Name = o.Vendor.Name
                                           
                                        }
                                    }).ToList();

                return returnresult;

            }
            catch (Exception ex) 
            {
                return new List<Purchaseorder>();
            }
        }



        public async Task<int> AddPurchaseOrder(PurchaseOrderViewModel data)
        {
            try
            {
                
                var newPurchaseOrder = new Purchaseorder
                {
                    OrderNo = data.Purchaseorder.OrderNo,
                    OrderDate = data.Purchaseorder.OrderDate,
                    VendorId = data.Purchaseorder.VendorId,
                    Notes = data.Purchaseorder.Notes,
                    OrderValue = data.Purchaseorder.OrderValue
                };

                await _context.Purchaseorders.AddAsync(newPurchaseOrder);
                await _context.SaveChangesAsync(); 

               
                foreach (var detail in data.Purchaseorderdetail)
                {
                    var newPurchaseOrderDetail = new Purchaseorderdetail
                    {
                        PurchaseOrderId = newPurchaseOrder.Id,
                        MaterialId = detail.MaterialId,
                        Quantity = detail.Quantity,
                        Rate = detail.Rate,
                        Amount = detail.Amount,
                        ExpectedDate = detail.ExpectedDate
                    };

                    await _context.Purchaseorderdetails.AddAsync(newPurchaseOrderDetail);
                }

                await _context.SaveChangesAsync();
                return newPurchaseOrder.Id; 
            }
            catch (Exception ex)
            {
                
                return -1; 
            }
        }



        public async Task<Purchaseorder> GetPurchaseOrderById(int id)
        {
            try
            {
                var resultdata = await _context.Purchaseorders.FirstOrDefaultAsync(u => u.Id == id);

                
                return resultdata;

            }
            catch (Exception ex)
            {
                return new Purchaseorder();
            }
        }



        public async Task<List<Purchaseorderdetail>> GetPurchaseOrderDetilas(int id)
        {
            try
            {
                var resultdata = await _context.Purchaseorderdetails
                                       .Where(u => u.PurchaseOrderId == id)
                                       .ToListAsync();


                return resultdata;

            }
            catch (Exception ex)
            {
                return new List<Purchaseorderdetail>();
            }
        }



      



        public async Task<int> UpdatePurchaseOrder(PurchaseOrderViewModel data)
        {
            try
            {
               
                var existingPurchaseOrder = await _context.Purchaseorders
                    .Include(po => po.Purchaseorderdetails) 
                    .FirstOrDefaultAsync(po => po.Id == data.Purchaseorder.Id);

                if (existingPurchaseOrder == null)
                {
                    return -1; 
                }

         
                existingPurchaseOrder.OrderNo = data.Purchaseorder.OrderNo;
                existingPurchaseOrder.OrderDate = data.Purchaseorder.OrderDate;
                existingPurchaseOrder.VendorId = data.Purchaseorder.VendorId;
                existingPurchaseOrder.Notes = data.Purchaseorder.Notes;
                existingPurchaseOrder.OrderValue = data.Purchaseorder.OrderValue;

               
                var existingDetails = existingPurchaseOrder.Purchaseorderdetails.ToList();

             
                var detailsToRemove = existingDetails
                    .Where(d => !data.Purchaseorderdetail.Any(pd => pd.Id == d.Id))
                    .ToList();

               
                _context.Purchaseorderdetails.RemoveRange(detailsToRemove);

              
                foreach (var detail in data.Purchaseorderdetail)
                {
                    if (detail.Id > 0)
                    {
                       
                        var existingDetail = existingDetails.FirstOrDefault(d => d.Id == detail.Id);
                        if (existingDetail != null)
                        {
                            existingDetail.MaterialId = detail.MaterialId;
                            existingDetail.Quantity = detail.Quantity;
                            existingDetail.Rate = detail.Rate;
                            existingDetail.Amount = detail.Amount;
                            existingDetail.ExpectedDate = detail.ExpectedDate;
                        }
                    }
                    else
                    {
                       
                        var newPurchaseOrderDetail = new Purchaseorderdetail
                        {
                            PurchaseOrderId = existingPurchaseOrder.Id,
                            MaterialId = detail.MaterialId,
                            Quantity = detail.Quantity,
                            Rate = detail.Rate,
                            Amount = detail.Amount,
                            ExpectedDate = detail.ExpectedDate
                        };

                        await _context.Purchaseorderdetails.AddAsync(newPurchaseOrderDetail);
                    }
                }

                
                await _context.SaveChangesAsync();

                return existingPurchaseOrder.Id; 
            }
            catch (Exception ex)
            {
                
                return -1; 
            }
        }





        public async Task<int> DeletePurchaseOrder(int purchaseOrderId)
        {
            try
            {
               
                var existingPurchaseOrder = await _context.Purchaseorders
                    .Include(po => po.Purchaseorderdetails) 
                    .FirstOrDefaultAsync(po => po.Id == purchaseOrderId);

                if (existingPurchaseOrder == null)
                {
                    return -1; 
                }

               
                _context.Purchaseorderdetails.RemoveRange(existingPurchaseOrder.Purchaseorderdetails);

            
                _context.Purchaseorders.Remove(existingPurchaseOrder);

            
                await _context.SaveChangesAsync();

                return 1; 
            }
            catch (Exception ex)
            {
               
                return -1;
            }
        }



    }
}
