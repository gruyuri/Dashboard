using DashboardWpf.Core.Models;
using DashboardWpf.DataAccess;
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
        private Dictionary<string, List<Employee>> cacheEmployee = new Dictionary<string, List<Employee>>();

        public IList<DateTime> GetTourDates(string depoCode)
        {
            var result = new List<DateTime>();

            if (string.IsNullOrEmpty(depoCode))
                return result;

            var today = DateTime.Today;
            var nextMonday = today.AddDays(1);

            while (nextMonday.DayOfWeek != DayOfWeek.Monday)
                nextMonday = nextMonday.AddDays(1);

            result.AddRange(new DateTime[] { today, nextMonday, 
                nextMonday.AddDays(7), nextMonday.AddDays(10), 
                nextMonday.AddDays(15), nextMonday.AddDays(19) });

            return result;
        }

        public IList<Employee> GetDepoEmployees(string depoCode)
        {
            if (String.IsNullOrEmpty(depoCode))
                return new List<Employee>();

            if (!cacheEmployee.ContainsKey(depoCode))
            {
                var generatedEmployee = DemoEmployees(depoCode);
                cacheEmployee[depoCode] = generatedEmployee;
            }
            
            return cacheEmployee[depoCode];
        }

        public IList<Tour> GetDepoTours(string depoCode, DateTime date)
        {
            var result = new List<Tour>();
            var employees = GetDepoEmployees(depoCode).ToArray();

            if (string.IsNullOrEmpty(depoCode))
                return result;

            var reservedDates = GetTourDates(depoCode);

            if (!reservedDates.Contains(date))
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
                Date = date.AddDays(7),
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
                Date = date.AddDays(10),
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
                Date = date.AddDays(14),
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

        private List<Employee> DemoEmployees(string depoCode, int n = 30)
        {
            var result = new List<Employee>();

            result.Add(new Employee() { FirstName = "NN", Code = "", IsDummy = true });
            result.Add(new Employee() { FirstName = $"Zusteller {depoCode}", Code = $"{depoCode}103554", IsAvailableForSubstitution = true });

            for (int i = 0; i < n; i++)
            {
                var employee = new Employee()
                {
                    FirstName = GetRandomItem(firstNames),
                    LastName = GetRandomItem(lastNames),
                    Code = $"{depoCode}{(new Random()).Next(100000, 999999)}",
                    IsAvailableForSubstitution = (i % 3 == 0)
                };

                result.Add(employee);
            }
            //result.Add(new Employee() { Name = "Brigitta Kraft", Code = $"{depoCode}107964", IsAvailableForSubstitution = true });
            //result.Add(new Employee() { Name = "Daniel Hacket", Code = $"{depoCode}457215", IsAvailableForSubstitution = false });
            //result.Add(new Employee() { Name = "Margarett Mitchell", Code = $"{depoCode}035681", IsAvailableForSubstitution = true });
            //result.Add(new Employee() { Name = "Lucia Freiburg", Code = $"{depoCode}134516", IsAvailableForSubstitution = true });
            //result.Add(new Employee() { Name = "Samuel Edwards", Code = $"{depoCode}123517", IsAvailableForSubstitution = false });

            return result.OrderBy(x => x.IsDummy)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.FirstName).ToList();
        }

        private string[] firstNames = new string[]
        {
            "Klaus-Dieter", "Justus", "Morten", "Frauke", "Frank", "Livia", "Maximilian", "Konrad", "Gerhard",
            "Michael", "Michaela", "Friederike", "Reiner", "Jochen", "Julia", "Lale", "Veronika", "Oliver", "Volker",
            "David", "Katja", "Jan", "Jürgen", "Sabine", "Jennifer", "Johanna", "Antje", "Christiane", "Bettine",
            "Winand", "Johannes", "Rainer", "Ralf", "Marcus", "Patrick", "Sebastian", "Lucas", "Leon", "Luka",
            "Finn", "Tobias", "Jonas", "Ben", "Elias", "Emma", "Anna", "Lena", "Julia", "Lea", "Hannah", "Laura", "Mia",
            "Karl", "Stefan", "Walter", "Gregor", "Uwe", "Hans", "Klaus", "Günter", "Ursula", "Christina", "Ilse",
            "Ingrid", "Petra", "Monika", "Gisela", "Susanne", "Horst", "Roman", "Silke"
        };

        private string[] lastNames = new string[]
        {
            "Frankenberger", "Bender", "Freidel", "Steffens", "Pergande", "Gerster", "Lachner", "Schuller", "Gnauk",
            "Martens", "Wiegel", "Haupt", "Burger", "Buchsteiner", "Schaaf", "Artun", "Grimm", "Bäte", "Looman",
            "Kampmann", "Gelinksy", "Bazing", "Dollase", "Spieler", "Wiebking", "Kalbitzer", "Dürrholz", "Biehler",
            "Heil", "Weiguny", "von Petersdorf", "Pennekamp", "Hank", "Bollmann", "Theurer", "Bernau", "Baltzter",
            "Bauchmüller", "Braun", "Deininger", "Kippes", "Lowitz", "Hansen", "Bauer", "Özkök", "Wagner", "Winkler",
            "Niemayer", "Unterstöger"
        };

        private string GetRandomItem(IList<string> items)
        {
            int idx = (new Random()).Next(0, items.Count);
            return items[idx];
        }

        public IList<Depot> GetAllDepot()
        {
            using (var p = new Persistenz())
            {
                var depoList = p.Session.CreateQuery("FROM Depot")
                        .List<DashboardWpf.DataAccess.Models.Depot>();

                IList<Depot> result = depoList.Select(x => new Depot() { Code = x.Code, Name = x.Name }).ToList();
                
                return result.OrderBy(x => x.Name)
                    .ToList();
            }

            //return DemoDepots();
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
