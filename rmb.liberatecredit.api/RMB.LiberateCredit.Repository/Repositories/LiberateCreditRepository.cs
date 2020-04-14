using RMB.LiberateCredit.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Repository.Repositories
{
    public class LiberateCreditRepository : DapperRepository, ILiberateCreditRepository
    {
        public LiberateCreditRepository() 
        {
        }
    }
}
