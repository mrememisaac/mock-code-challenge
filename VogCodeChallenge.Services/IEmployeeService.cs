using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();

        Task<List<Employee>> GetEmployeesByDepartment(int departmentId);

        Task<EmployeesApiViewModel> GetAll(int page, int recordsPerPage);
    }
}
