using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DashboardWpf.Services
{
    public class DepotService : IDepotService
    {
        public IList<Employee> GetDepoEmployees(string depoCode)
        {
            return DemoEmployees(depoCode);
        }

        public IList<Tour> GetDepoTours(string depoCode, DateTime date)
        {
            var result = new List<Tour>();
            var employees = DemoEmployees(depoCode).ToArray();

            if (date.DayOfWeek == DayOfWeek.Sunday)
                return result;

            Tour komplettTour1 = new Tour()
            {
                Date = date,
                Name = "001",
                FactH = 6,
                PlanH = 8,
                Boxes = new ObservableCollection<Box>( new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[0] },
                    new Box() { BoxNumber = "02", Employee = employees[0] },
                    new Box() { BoxNumber = "03", Employee = employees[0] },
                    new Box() { BoxNumber = "04", Employee = employees[0] },
                    new Box() { BoxNumber = "05", Employee = employees[0] }
                })
            };

            Tour komplettTour2 = new Tour()
            {
                Date = date,
                Name = "002",
                FactH = 8,
                PlanH = 8,
                Boxes = new ObservableCollection<Box>(new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[1] },
                    new Box() { BoxNumber = "02", Employee = employees[1] },
                    new Box() { BoxNumber = "03", Employee = employees[1] },
                    new Box() { BoxNumber = "04", Employee = employees[1] },
                    new Box() { BoxNumber = "05", Employee = employees[1] }
                })
            };

            Tour leerTour1 = new Tour()
            {
                Date = date,
                Name = "003",
                FactH = 0,
                PlanH = 8,
                Boxes = new ObservableCollection<Box>(new List<Box>()
                {
                    new Box() { BoxNumber = "01" },
                    new Box() { BoxNumber = "02" },
                    new Box() { BoxNumber = "03" },
                    new Box() { BoxNumber = "04" },
                    new Box() { BoxNumber = "05" },
                    new Box() { BoxNumber = "06" },
                    new Box() { BoxNumber = "07" },
                })
            };

            Tour leerTour2 = new Tour()
            {
                Date = date,
                Name = "005",
                FactH = 0,
                PlanH = 8,
                Boxes = new ObservableCollection<Box>(new List<Box>()
                {
                    new Box() { BoxNumber = "01" },
                    new Box() { BoxNumber = "02" },
                    new Box() { BoxNumber = "03" },
                    new Box() { BoxNumber = "04" },
                    new Box() { BoxNumber = "05" },
                    new Box() { BoxNumber = "06" },
                })
            };

            Tour mixTour1 = new Tour()
            {
                Date = date,
                Name = "004",
                FactH = 3,
                PlanH = 8,
                Boxes = new ObservableCollection<Box>(new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[2] },
                    new Box() { BoxNumber = "02", Employee = employees[2] },
                    new Box() { BoxNumber = "03", Employee = employees[3] },
                    new Box() { BoxNumber = "04", Employee = employees[3] },
                    new Box() { BoxNumber = "05", Employee = employees[4] },
                    new Box() { BoxNumber = "06", Employee = employees[4] }
                })
            };

            Tour mixTour2 = new Tour()
            {
                Date = date,
                Name = "006",
                FactH = 3,
                PlanH = 8,
                Boxes = new ObservableCollection<Box>(new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[2] },
                    new Box() { BoxNumber = "02" },
                    new Box() { BoxNumber = "03", Employee = employees[5] },
                    new Box() { BoxNumber = "04" },
                    new Box() { BoxNumber = "05", Employee = employees[6] },
                    new Box() { BoxNumber = "06" }
                })
            };

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    result.AddRange(new Tour[] { komplettTour1, leerTour1, mixTour1 });
                    break;

                case DayOfWeek.Monday:
                    result.AddRange(new Tour[] { komplettTour2, leerTour2, mixTour1, mixTour2 });
                    break;

                default:
                    result.AddRange(new Tour[] { komplettTour1, komplettTour2, leerTour1, leerTour2, mixTour1, mixTour2 });
                    break;
            }

            return result;
        }

        private List<Employee> DemoEmployees(string depoCode)
        {
            var result = new List<Employee>();

            result.Add(new Employee() { Name = "NN", Code = "", IsDummy = true });
            result.Add(new Employee() { Name = $"Zusteller {depoCode}", Code = $"{depoCode}103554", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Abraham Brams", Code = $"{depoCode}152372", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Brigitta Kraft", Code = $"{depoCode}107964", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Daniel Hacket", Code = $"{depoCode}457215", IsAvailableForSubstitution = false });
            result.Add(new Employee() { Name = "Margarett Mitchell", Code = $"{depoCode}035681", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Lucia Freiburg", Code = $"{depoCode}134516", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Samuel Edwards", Code = $"{depoCode}123517", IsAvailableForSubstitution = false });

            return result.OrderBy(x => x.IsDummy)
                .ThenBy(x => x.Name).ToList();
        }

        public IList<Depot> GetAllDepot()
        {
            return DemoDepots();
        }

        private List<Depot> DemoDepots()
        {
            var result = new List<Depot>();

            result.Add(new Depot() { Name = "Zehlendorf", Code = "01" });
            result.Add(new Depot() { Name = "Steglitz", Code = "02" });
            result.Add(new Depot() { Name = "Wilmersdorf", Code = "03" });
            result.Add(new Depot() { Name = "Spandau", Code = "04" });
            result.Add(new Depot() { Name = "Charlottenburg", Code = "05" });
            result.Add(new Depot() { Name = "Schöneberg", Code = "06" });
            result.Add(new Depot() { Name = "Tempelhof", Code = "07" });
            result.Add(new Depot() { Name = "City", Code = "08" });
            result.Add(new Depot() { Name = "Rudow", Code = "09" });
            result.Add(new Depot() { Name = "Pin Mail", Code = "10" });
            result.Add(new Depot() { Name = "Neukölln", Code = "11" });
            result.Add(new Depot() { Name = "Wedding", Code = "12" });
            result.Add(new Depot() { Name = "Reinickendorf", Code = "13" });
            result.Add(new Depot() { Name = "Lichtenberg", Code = "14" });
            result.Add(new Depot() { Name = "Prenzlauer Berg", Code = "15" });
            result.Add(new Depot() { Name = "Weißensee", Code = "16" });
            result.Add(new Depot() { Name = "Köpenick", Code = "17" });
            result.Add(new Depot() { Name = "Marzahn-Hellersdorf", Code = "18" });

            return result.OrderBy(x => x.Name)
                .ToList();
        }
    }
}
