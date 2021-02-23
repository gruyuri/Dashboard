using DashboardWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Services.Interfaces
{
    public interface IDepotService
    {
        public IList<Tour> GetDepoTours(string depoCode, DateTime date);

        public IList<Employee> GetDepoEmployees(string depoCode);
    }
}
