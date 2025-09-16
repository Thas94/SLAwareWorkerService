using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAwareWorkerService.Enums
{
    public class Enums
    {
        public enum TicketStatus
        {
            New = 1,
            Assigned = 2,
            InProgress = 3,
            AwaitingFeedback = 4,
            Resolved = 5,
            Closed = 6,
        }
    }
}
