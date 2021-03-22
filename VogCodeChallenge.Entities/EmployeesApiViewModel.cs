using System;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.Entities
{
    public class EmployeesApiViewModel
    {
        public int TotalRecordCount { get; set; }

        public int Page { get; set; }

        public int RecordsPerPage { get; set; }

        public List<Employee> Data { get; set; }
    }
}
