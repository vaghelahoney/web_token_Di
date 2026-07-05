using web_token_Di.Models.DTOs;

namespace web_token_Di.Repositories
{
    public interface IEmployeeRepositories
    {
        Task<IList<EmployeeModel>> GetAllEmployeeAsync();

        Task<EmployeeModel?> GetEmployeeByIdAsync(int id);

        Task<EmployeeModel> AddEmployeeAsync(EmployeeModel entity);

        Task<bool> UpdateEmployeeAsync(EmployeeModel entity);

        Task<bool> DeleteEmployeeAsync(int id);

        Task<bool> FindDublicateEmployeeAsync(string name,int id);
    }
}
