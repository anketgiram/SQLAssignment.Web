using Assignment.DataLayar.Contract;
using Assignmnet.EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Web.Controllers
{
    public class PurchaseController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly ApplicationDbContext _context;
        public PurchaseController(IUnitOfWork db, ApplicationDbContext context)
        {
            _unitOfWork = db;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var getpurchaseorder = await _unitOfWork.PurchaseService.GetPurchaseOrder();
                return View(getpurchaseorder);
            }
            catch (Exception ex) 
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> CreatePurchaseOrder()
        {
            try
            {
                ViewBag.Vendoredata = await _unitOfWork.VendorService.GetActiveVendor();
                ViewBag.Materialdata = await _unitOfWork.MaterialService.GetActiveMaterial();

                var model = new PurchaseOrderViewModel
                {
                    Purchaseorder = new Purchaseorder(),
                    Purchaseorderdetail = new List<Purchaseorderdetail>() 
                };

                return View(model);

            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderViewModel data)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        var addPurchaseOrder = await _unitOfWork.PurchaseService.AddPurchaseOrder(data);

                        if (addPurchaseOrder >= 1)
                        {
                            await transaction.CommitAsync();
                            TempData["success"] = "Purchase Order created successfully!";
                            TempData["Valid"] = "0";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception("Failed to create Purchase Order.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid Purchase Order data.");
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["error"] = "Failed to add record.";
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditPurchaseOrder(int id)
        {
            try
            {
                ViewBag.Vendoredata = await _unitOfWork.VendorService.GetActiveVendor();
                ViewBag.Materialdata = await _unitOfWork.MaterialService.GetActiveMaterial();


                var Purchaseorderdata = await _unitOfWork.PurchaseService.GetPurchaseOrderById( id);

                if(Purchaseorderdata == null)
                {
                    throw new Exception("Data Not Found");
                }


                var purchasedetilasorder = await _unitOfWork.PurchaseService.GetPurchaseOrderDetilas(Purchaseorderdata.Id);

                if(purchasedetilasorder.Count() == 0)
                {
                    throw new Exception("Data Not Found");
                }


                var model = new PurchaseOrderViewModel
                {
                    Purchaseorder = Purchaseorderdata,
                    Purchaseorderdetail = purchasedetilasorder
                };

                return View(model);
            }
            catch(Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }


        }




        [HttpPost]
        public async Task<IActionResult> EditPurchaseOrder(PurchaseOrderViewModel data)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        var updatePurchaseOrder = await _unitOfWork.PurchaseService.UpdatePurchaseOrder(data);

                        if (updatePurchaseOrder >= 1)
                        {
                            await transaction.CommitAsync();
                            TempData["success"] = "Purchase Order created successfully!";
                            TempData["Valid"] = "0";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception("Failed to create Purchase Order.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid Purchase Order data.");
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["error"] = "Failed to add record.";
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");
                }
            }
        }





        [HttpGet]
        public async Task<ActionResult> ViewPurchaseOrder(int id)
        {
            try
            {
                ViewBag.Vendoredata = await _unitOfWork.VendorService.GetActiveVendor();
                ViewBag.Materialdata = await _unitOfWork.MaterialService.GetActiveMaterial();


                var Purchaseorderdata = await _unitOfWork.PurchaseService.GetPurchaseOrderById(id);

                if (Purchaseorderdata == null)
                {
                    throw new Exception("Data Not Found");
                }


                var purchasedetilasorder = await _unitOfWork.PurchaseService.GetPurchaseOrderDetilas(Purchaseorderdata.Id);

                if (purchasedetilasorder.Count() == 0)
                {
                    throw new Exception("Data Not Found");
                }


                var model = new PurchaseOrderViewModel
                {
                    Purchaseorder = Purchaseorderdata,
                    Purchaseorderdetail = purchasedetilasorder
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }


        }




        [HttpGet]
        public async Task<ActionResult>  DeletePurchaseOrder(int id)
        {
            try
            {
                ViewBag.Vendoredata = await _unitOfWork.VendorService.GetActiveVendor();
                ViewBag.Materialdata = await _unitOfWork.MaterialService.GetActiveMaterial();


                var Purchaseorderdata = await _unitOfWork.PurchaseService.GetPurchaseOrderById(id);

                if (Purchaseorderdata == null)
                {
                    throw new Exception("Data Not Found");
                }


                var purchasedetilasorder = await _unitOfWork.PurchaseService.GetPurchaseOrderDetilas(Purchaseorderdata.Id);

                if (purchasedetilasorder.Count() == 0)
                {
                    throw new Exception("Data Not Found");
                }


                var model = new PurchaseOrderViewModel
                {
                    Purchaseorder = Purchaseorderdata,
                    Purchaseorderdetail = purchasedetilasorder
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }


        }




        [HttpPost]
        public async Task<IActionResult> DeletePurchaseOrder(PurchaseOrderViewModel data)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        var updatePurchaseOrder = await _unitOfWork.PurchaseService.DeletePurchaseOrder(data.Purchaseorder.Id);

                        if (updatePurchaseOrder >= 1)
                        {
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();
                            TempData["success"] = "Purchase Order Deleted !";
                            TempData["Valid"] = "0";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception("Failed to DeletedDeleted Purchase Order.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid Purchase Order data.");
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["error"] = "Failed to Deleted record.";
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");
                }
            }
        }


    }
}
