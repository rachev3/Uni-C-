﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirCompanySystem.DataContext.Contracts
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
