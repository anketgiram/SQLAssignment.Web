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
    public class VendorService: IVendorService
    {
        private readonly ApplicationDbContext _context;

        public VendorService(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<int> AddVendor(Vendor data)
        {
            int status;
            try
            {
                var newVendor = new Vendor()
                {
                    Name = data.Name,
                    Code = data.Code,
                    AddressLine1 = data.AddressLine1,
                    AddressLine2 = data.AddressLine2,
                    ContactEmail = data.ContactEmail,
                    ContactNo = data.ContactNo,
                    ValidTillDate = data.ValidTillDate,
                    IsActive = data.IsActive,
                };

                var result = await this._context.Vendors.AddAsync(newVendor);


                await _context.Vendors.AddAsync(newVendor);
                await _context.SaveChangesAsync(); 

                return newVendor.Id; 

                
            }
            catch (Exception ex) 
            {
                return -1;
            }

        }

        
        public async Task<List<Vendor>> GetVendor()
        {
            try
            {
                return await _context.Vendors.ToListAsync();
              
            }
            catch
            {
                return new List<Vendor>();
            }
        }

        public async Task<Vendor> GetVendorById(int id)
        {
            try
            {
                return await _context.Vendors.FirstOrDefaultAsync(u=> u.Id == id);
            }
            catch
            {
                return new Vendor();
            }
        }




        public async Task<int> EditVendor(Vendor data)
        {
            try
            {
                
                var existingVendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == data.Id);

                if (existingVendor == null)
                {
                    
                    return -1;
                }

                
                existingVendor.Name = data.Name;
                existingVendor.Code = data.Code;
                existingVendor.AddressLine1 = data.AddressLine1;
                existingVendor.AddressLine2 = data.AddressLine2;
                existingVendor.ContactEmail = data.ContactEmail;
                existingVendor.ContactNo = data.ContactNo;
                existingVendor.ValidTillDate = data.ValidTillDate;
                existingVendor.IsActive = data.IsActive;

                await _context.SaveChangesAsync();

                return 1; 
            }
            catch (Exception ex)
            {
               
                return -1; 
            }
        }




        public async Task<int> DeleteVendorById(int id)
        {
            try
            {
               
                var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);

                if (vendor == null)
                {
                    
                    return -1;
                }
                _context.Vendors.Remove(vendor);

             
                return 1;
            }
            catch (Exception ex)
            {
              
                return -1; 
            }
        }


        public async Task<List<Vendor>> GetActiveVendor()
        {
            try
            {
                return await _context.Vendors.Where(u => u.IsActive == true).ToListAsync();

            }
            catch
            {
                return new List<Vendor>();
            }
        }


    }
}


