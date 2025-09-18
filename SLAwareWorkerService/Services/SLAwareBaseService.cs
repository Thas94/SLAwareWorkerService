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
        public static readonly TimeSpan WorkStart = new TimeSpan(8, 30, 0);
        public static readonly TimeSpan WorkEnd = new TimeSpan(17, 0, 0);
        public SLAwareBaseService(slaware_dataContext slaware_DataContext)
        {
            _slaware_DataContext = slaware_DataContext;
        }

        public bool IsWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday ? true : false;
        }
        public bool IsWorkingHours(DateTime date)
        {
            return date.TimeOfDay >= WorkStart && date.TimeOfDay <= WorkEnd ? true : false;
        }
    }
}
