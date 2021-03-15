using DashboardWpf.DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.DataAccess.Mapping
{
    public class DepotMap: ClassMap<Depot>
    {
        public DepotMap()
        {
            Table("depot");

            Id(x => x.Id);
            Map(x => x.Version);

            Map(x => x.Code);
            Map(x => x.Name);
        }
    }
}
