using System;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.Entities
{
    public class Employee : EntityBase, IEntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

    }
}
