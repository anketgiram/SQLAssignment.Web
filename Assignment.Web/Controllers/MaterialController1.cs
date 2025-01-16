using Assignment.DataLayar.Contract;
using Assignmnet.EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Controllers
{
    public class MaterialController1 : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly ApplicationDbContext _context;
        public MaterialController1(IUnitOfWork db, ApplicationDbContext context)
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
                    var data = await _unitOfWork.MaterialService.GetMaterial();
                    return View(data);

                }
                catch (Exception ex)
                {
                    return View();
                }
            }

        }

        [HttpGet]
        public async Task<IActionResult> CreateMaterial()
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
        public async Task<IActionResult> CreateMaterial(Material data)
        {
            using (var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        var addvendor = await _unitOfWork.MaterialService.AddMaterial(data);

                        if (addvendor >= 1)
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
                catch (Exception ex)
                {
                    transcation.RollbackAsync();
                    TempData["sucess"] = "Faild Add Record";
                    TempData["Valid"] = "0";
                    return RedirectToAction("Index");

                }

            }
        }



        [HttpGet]
        public async Task<IActionResult> EditMaterial(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    throw new Exception("Data Not Found");
                }
                else
                {
                    var getvendor = await _unitOfWork.MaterialService.GetMaterialById(id);
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
        public async Task<IActionResult> EditMaterial(Material data)
        {
            using (var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        var editvendor = await _unitOfWork.MaterialService.EditMaterial(data);

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
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    throw new Exception("Data Not Found");
                }
                else
                {
                    var getvendor = await _unitOfWork.MaterialService.GetMaterialById(id);
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
        public async Task<IActionResult> DeleteMaterial(Vendor data)
        {
            using (var transcation = _context.Database.BeginTransaction())
            {

                try
                {
                    if (data.Id != null || data.Id == 0)
                    {

                        var getvendor = await _unitOfWork.MaterialService.DeleteMaterialById(data.Id);
                        if (getvendor == 1)
                        {
                            transcation.CommitAsync();
                            _context.SaveChangesAsync();
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
        public async Task<IActionResult> ViewMaterial(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    throw new Exception("Data Not Found");
                }
                else
                {
                    var getvendor = await _unitOfWork.MaterialService.GetMaterialById(id);
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
