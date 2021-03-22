using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VogCodeChallenge.Entities
{
    public class Department : EntityBase, IEntityBase, IEquatable<Department>
    {
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public Department()
        {
            //initialized so that we always have a ready to use(non-null) Employees property
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
