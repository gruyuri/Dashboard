using DashboardWpf.DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.DataAccess.Mapping
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("employee");

            Id(x => x.Id);
            Map(x => x.Version);

            Map(x => x.Code);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.IsReadyForSubstitution).Column("isreadysubstitute");

            References(x => x.Depot).Column("depot_id");
        }
    }
}
