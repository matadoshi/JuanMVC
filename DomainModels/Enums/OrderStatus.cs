﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.DomainModels.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Accepted,
        Rejected,
        Sipped,
        Courrier
    }
}
