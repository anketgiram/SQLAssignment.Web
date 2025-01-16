using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataLayar.Contract
{
    public interface IRepository<T> where T : class
    {

        public Task AddAsync(T entity);
        public Task<int> SaveChangesAsync();
    }
}
