using DashboardWpf.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardWpf.Core.Models
{
    public class Tour
    {
        public DateTime Date { get; set; } = DateTime.Today;

        public string Name { get; set; }

        public IList<Box> Boxes { get; set; } = new List<Box>();

        public int PlanH { get; set; } = 8;

        public int FactH { get; set; } = 0;

        public TourStatus Status
        {
            get
            {
                if (Boxes.All(x => x.IsAssigned))
                    return TourStatus.Komplett;

                if (Boxes.All(x => !x.IsAssigned))
                    return TourStatus.Liegend;

                return TourStatus.Mix;
            }
        }

        public string MainEmployee
        {
            get
            {
                switch (this.Status)
                {
                    case TourStatus.Komplett:
                        return Boxes.FirstOrDefault()?.Employee?.Name;

                    case TourStatus.Mix:
                        return "MIX";

                    default:
                        return "NN";
                }
            }
        }
    }
}
