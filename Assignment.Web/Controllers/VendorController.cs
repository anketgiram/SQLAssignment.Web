using Assignment.DataLayar.Contract;
using Microsoft.AspNetCore.Mvc;
using Assignmnet.EntityLayer.Models;

namespace Assignment.Web.Controllers
{
    public class VendorController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly ApplicationDbContext _context;
        public VendorController(IUnitOfWork db, ApplicationDbContext context)
        {
            _unitOfWork = db;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            using (var transcation = _context.Database.BeginTransaction())
            {

                try
                {
                    var data = await _unitOfWork.VendorService.GetVendor();
                    return View(data);

                }
                catch (Exception ex)
                {
                    return View();
                }
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> CreateVendor()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateVendor(Vendor data)
        {
            using (var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    if(data != null)
                    {
                        var addvendor = await _unitOfWork.VendorService.AddVendor(data);

                        if(addvendor >= 1)
                        {
                            transcation.CommitAsync();
                            _context.SaveChangesAsync();
                            TempData["sucess"] = "Record Add Sccessfully";
                            TempData["Valid"] = "0";
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            throw new Exception("Data not Insrted");
                        }
                    }
                    else
                    {
                        throw new Exception("Data not found");
                    }
                }
                catch(Exception ex)
                {
                    transcation.RollbackAsync();
                    TempData["sucess"] = "Faild Add Record";
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");

                }

            }
        }



        [HttpGet]
        public async Task<IActionResult> EditVendor(int id)
        {
            try
            {
                if(id == null || id == 0)
                {
                    throw new Exception("Data Not Found");
                }
                else
                {
                    var getvendor = await _unitOfWork.VendorService.GetVendorById(id);
                    return View(getvendor);
                }
               

            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }
        }



        [HttpPost]
        public async Task<IActionResult> EditVendor(Vendor data)
        {
            using (var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        var editvendor = await _unitOfWork.VendorService.EditVendor(data);

                        if (editvendor == 1)
                        {
                            transcation.CommitAsync();
                            _context.SaveChangesAsync();
                            TempData["sucess"] = "Record edited Sccessfully";
                            TempData["Valid"] = "0";
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            throw new Exception("Data not edited");
                        }
                    }
                    else
                    {
                        throw new Exception("Data not edited");
                    }
                }
                catch (Exception ex)
                {
                    transcation.RollbackAsync();
                    TempData["sucess"] = "Faild edit Record";
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");

                }

            }
        }



        [HttpGet]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    throw new Exception("Data Not Found");
                }
                else
                {
                    var getvendor = await _unitOfWork.VendorService.GetVendorById(id);
                    return View(getvendor);
                }


            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }
        }




        [HttpPost]
        public async Task<IActionResult> DeleteVendor(Vendor data)
        {
            using (var transcation = _context.Database.BeginTransaction())
            {

                try
                {
                    if (data.Id != null || data.Id == 0)
                    { 
                    
                        var getvendor = await _unitOfWork.VendorService.DeleteVendorById(data.Id);
                        if (getvendor == 1)
                        {
                          
                           await _context.SaveChangesAsync();
                           await transcation.CommitAsync();
                            TempData["sucess"] = "Record edited Sccessfully";
                            TempData["Valid"] = "0";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception("unable to delete the record");
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Data Not Found");
                    }


                }
                catch (Exception ex)
                {
                    transcation.RollbackAsync();
                    TempData["sucess"] = ex.Message;
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");
                }
            }
        }



        [HttpGet]
        public async Task<IActionResult> ViewVendor(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    throw new Exception("Data Not Found");
                }
                else
                {
                    var getvendor = await _unitOfWork.VendorService.GetVendorById(id);
                    return View(getvendor);
                }


            }
            catch (Exception ex)
            {
                TempData["sucess"] = ex.Message;
                TempData["Valid"] = "0";
                return RedirectToAction("Index");
            }
        }


    }
}
