using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class TicketStatus
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<TicketActivityLog> TicketActivityLogNewTicketStatuses { get; set; } = new List<TicketActivityLog>();

    public virtual ICollection<TicketActivityLog> TicketActivityLogOldTicketStatuses { get; set; } = new List<TicketActivityLog>();
}
