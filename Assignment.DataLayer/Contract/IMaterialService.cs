using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignmnet.EntityLayer.Models;

namespace Assignment.DataLayar.Contract
{
    public interface IMaterialService
    {
        public Task<int> AddMaterial(Material data);
        public Task<List<Material>> GetMaterial();
        public Task<Material> GetMaterialById(int id);
        public Task<int> EditMaterial(Material data);
        public Task<int> DeleteMaterialById(int id);
        public Task<List<Material>> GetActiveMaterial();
    }
}
