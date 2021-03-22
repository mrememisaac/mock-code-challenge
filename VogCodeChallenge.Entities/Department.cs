using System;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.Entities
{
    public class Department : EntityBase, IEntityBase
    {
        public virtual ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
        }
    }
}
