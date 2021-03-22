using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.Services
{
    public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();
    }
}
