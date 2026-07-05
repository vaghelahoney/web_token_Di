using Microsoft.EntityFrameworkCore;
using web_token_Di.Data;
using web_token_Di.Models.DTOs;

namespace web_token_Di.Repositories
{
    public class EmployeeRepositories : IEmployeeRepositories
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel entity)
        {
            await _context.Employee.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity; 
        }

        public  async Task<bool> DeleteEmployeeAsync(int id)
        {
           var data =  await _context.Employee.FindAsync(id);

            if (data != null)
            {
                _context.Employee.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> FindDublicateEmployeeAsync(string name, int id)
        {
            if (string.IsNullOrWhiteSpace(name)) {
            
                 return  await _context.Employee.AnyAsync(x => Convert.ToString(x.Name) == Convert.ToString(name) && x.Id != id);
            }
            return false;
        }

        public async Task<IList<EmployeeModel>> GetAllEmployeeAsync()
        {
           return await _context.Employee.ToListAsync();

        }

        public async Task<EmployeeModel?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employee.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeModel entity)
        {
            if(entity.Id <= 0)
            {
                return false;
            }

            if(entity != null)
            {
                _context.Employee.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
