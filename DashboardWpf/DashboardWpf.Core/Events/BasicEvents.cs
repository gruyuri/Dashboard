﻿using DashboardWpf.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.Core.Events
{
    public class ModuleSelected: PubSubEvent<string>
    {
    }

    public class DepotSelected : PubSubEvent<Depot>
    {
    }
}
