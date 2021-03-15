using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.DataAccess.Models
{
    public class Employee: BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual bool IsReadyForSubstitution { get; set; }
        public virtual Depot Depot { get; set; }
    }
}
