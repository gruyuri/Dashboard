using DashboardWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Services.Interfaces
{
    public interface IDepoService
    {
        public IList<Tour> GetDepoTours(string depoCode);

        public IList<Employee> GetDepoEmployees(string depoCode);
    }
}
