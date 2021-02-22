using DashboardWpf.Core.Models;
using DashboardWpf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Services
{
    public class DepoService : IDepoService
    {
        public IList<Employee> GetDepoEmployees(string depoCode)
        {
            return DemoEmployees();
        }

        public IList<Tour> GetDepoTours(string depoCode, DateTime date)
        {
            var result = new List<Tour>();
            var employees = DemoEmployees().ToArray();

            if (date.DayOfWeek == DayOfWeek.Sunday)
                return result;

            Tour komplettTour1 = new Tour()
            {
                Date = date,
                Name = "001",
                FactH = 6,
                PlanH = 8,
                Boxes = new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[0] },
                    new Box() { BoxNumber = "02", Employee = employees[0] },
                    new Box() { BoxNumber = "03", Employee = employees[0] },
                    new Box() { BoxNumber = "04", Employee = employees[0] },
                    new Box() { BoxNumber = "05", Employee = employees[0] }
                }
            };

            Tour komplettTour2 = new Tour()
            {
                Date = date,
                Name = "002",
                FactH = 8,
                PlanH = 8,
                Boxes = new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[1] },
                    new Box() { BoxNumber = "02", Employee = employees[1] },
                    new Box() { BoxNumber = "03", Employee = employees[1] },
                    new Box() { BoxNumber = "04", Employee = employees[1] },
                    new Box() { BoxNumber = "05", Employee = employees[1] }
                }
            };

            Tour leerTour1 = new Tour()
            {
                Date = date,
                Name = "003",
                FactH = 0,
                PlanH = 8,
                Boxes = new List<Box>()
                {
                    new Box() { BoxNumber = "01" },
                    new Box() { BoxNumber = "02" },
                    new Box() { BoxNumber = "03" },
                    new Box() { BoxNumber = "04" },
                    new Box() { BoxNumber = "05" },
                    new Box() { BoxNumber = "06" },
                    new Box() { BoxNumber = "07" },
                }
            };

            Tour leerTour2 = new Tour()
            {
                Date = date,
                Name = "005",
                FactH = 0,
                PlanH = 8,
                Boxes = new List<Box>()
                {
                    new Box() { BoxNumber = "01" },
                    new Box() { BoxNumber = "02" },
                    new Box() { BoxNumber = "03" },
                    new Box() { BoxNumber = "04" },
                    new Box() { BoxNumber = "05" },
                    new Box() { BoxNumber = "06" },
                }
            };

            Tour mixTour1 = new Tour()
            {
                Date = date,
                Name = "004",
                FactH = 3,
                PlanH = 8,
                Boxes = new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[2] },
                    new Box() { BoxNumber = "02", Employee = employees[2] },
                    new Box() { BoxNumber = "03", Employee = employees[3] },
                    new Box() { BoxNumber = "04", Employee = employees[3] },
                    new Box() { BoxNumber = "05", Employee = employees[4] },
                    new Box() { BoxNumber = "06", Employee = employees[4] }
                }
            };

            Tour mixTour2 = new Tour()
            {
                Date = date,
                Name = "006",
                FactH = 3,
                PlanH = 8,
                Boxes = new List<Box>()
                {
                    new Box() { BoxNumber = "01", Employee = employees[2] },
                    new Box() { BoxNumber = "02" },
                    new Box() { BoxNumber = "03", Employee = employees[5] },
                    new Box() { BoxNumber = "04" },
                    new Box() { BoxNumber = "05", Employee = employees[6] },
                    new Box() { BoxNumber = "06" }
                }
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

        private List<Employee> DemoEmployees()
        {
            var result = new List<Employee>();

            result.Add(new Employee() { Name = "Zusteller A", Code = "XXX1", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Abraham Brams", Code = "XXX2", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Brigitta Kraft", Code = "XXX3", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Daniel Hacket", Code = "XXX4", IsAvailableForSubstitution = false });
            result.Add(new Employee() { Name = "Margarett Mitchell", Code = "XXX5", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Lucia Freiburg", Code = "XXX6", IsAvailableForSubstitution = true });
            result.Add(new Employee() { Name = "Samuel Edwards", Code = "XXX7", IsAvailableForSubstitution = false });

            return result;
        }
    }
}
