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
    public class MaterialService: IMaterialService
    {
        private readonly ApplicationDbContext _context;

        public MaterialService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<int> AddMaterial(Material data)
        {
            int status;
            try
            {
                var newMaterial = new Material()
                {
                  
                    Code = data.Code,
                    ShortText = data.ShortText,
                    LongTexts = data.LongTexts,
                    Unit = data.Unit,
                    ReorderLevel = data.ReorderLevel,
                    MinOrderQuantity = data.MinOrderQuantity,
                    IsActive = data.IsActive,
                };

                var result = await this._context.Materials.AddAsync(newMaterial);


                await _context.Materials.AddAsync(newMaterial);
                await _context.SaveChangesAsync();

                return newMaterial.Id;


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> DeleteMaterialById(int id)
        {
            try
            {

                var material = await _context.Materials.FirstOrDefaultAsync(v => v.Id == id);

                if (material == null)
                {

                    return -1;
                }
                _context.Materials.Remove(material);


                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public async Task<int> EditMaterial(Material data)
        {
            try
            {

                var existingMaterial = await _context.Materials.FirstOrDefaultAsync(v => v.Id == data.Id);

                if (existingMaterial == null)
                {

                    return -1;
                }


               
                existingMaterial.Code = data.Code;
                existingMaterial.ShortText = data.ShortText;
                existingMaterial.LongTexts = data.LongTexts;
                existingMaterial.Unit = data.Unit;
                existingMaterial.ReorderLevel = data.ReorderLevel;
                existingMaterial.MinOrderQuantity = data.MinOrderQuantity;
                existingMaterial.IsActive = data.IsActive;

                await _context.SaveChangesAsync();

                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public async Task<List<Material>> GetMaterial()
        {
            try
            {
                return await _context.Materials.ToListAsync();

            }
            catch
            {
                return new List<Material>();
            }
        }

        public async Task<Material> GetMaterialById(int id)
        {
            try
            {
                return await _context.Materials.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch
            {
                return new Material();
            }
        }


        public async Task<List<Material>> GetActiveMaterial()
        {
            try
            {
                return await _context.Materials.Where(u => u.IsActive == true).ToListAsync();

            }
            catch
            {
                return new List<Material>();
            }
        }

    }
}
