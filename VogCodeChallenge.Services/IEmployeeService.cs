using System.Collections;
using System.Collections.Generic;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();

        IList<Employee> GetEmployeesByDepartment(int departmentId);
        IList<Employee> GetAll(int page, int recordsPerPage);
    }
}
