using DashboardWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Services.Interfaces
{
    public interface IDepotService
    {
        IList<Tour> GetDepoTours(string depoCode, DateTime date);

        IList<Employee> GetDepoEmployees(string depoCode);

        IList<Depot> GetAllDepot();

        IList<DateTime> GetTourDates(string depoCode);
    }
}
