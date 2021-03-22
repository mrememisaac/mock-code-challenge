using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace VogCodeChallenge.Entities
{
    public class Department : EntityBase, IEntityBase, IEquatable<Department>
    {
        public virtual ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
        }

        public bool Equals([AllowNull] Department other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Department department && Equals(department);
        }
    }
}
