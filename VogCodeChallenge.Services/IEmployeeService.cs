using System.Collections;

namespace VogCodeChallenge.Services
{
    public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();
    }
}
