using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.DataAccess.Models
{
    public class BaseEntity
    {
        public virtual long? Id { get; set; }
        public virtual long Version { get; set; }
    }
}
