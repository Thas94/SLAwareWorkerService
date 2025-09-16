using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAwareWorkerService.Entities.SLAware;

namespace SLAwareWorkerService.Services
{
    public class SLAwareBaseService
    {
        public readonly slaware_dataContext _slaware_DataContext;
        public SLAwareBaseService(slaware_dataContext slaware_DataContext)
        {
            _slaware_DataContext = slaware_DataContext;
        }
    }
}
