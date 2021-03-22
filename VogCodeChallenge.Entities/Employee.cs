using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace VogCodeChallenge.Entities
{
    public class Employee : EntityBase, IEntityBase, IEquatable<Employee>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public bool Equals([AllowNull] Employee other)
        {
            return other != null && Id == other.Id;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Employee e && Equals(e);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
