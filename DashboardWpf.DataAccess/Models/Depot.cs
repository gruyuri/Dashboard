using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.DataAccess.Models
{
    public class Depot: BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
    }
}
