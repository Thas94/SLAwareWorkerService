using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class Ticket
{
    public long Id { get; set; }

    public long TicketNumber { get; set; }

    public string Subject { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long TicketStatusId { get; set; }

    public long TicketSeverityLevelId { get; set; }

    public long CreatedById { get; set; }

    public long? AssignedToId { get; set; }

    public long ApplicationId { get; set; }

    public long TicketCategoryId { get; set; }

    public long TicketSubCategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<TicketActivityLog> TicketActivityLogs { get; set; } = new List<TicketActivityLog>();

    public virtual ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();

    public virtual ICollection<TicketNotification> TicketNotifications { get; set; } = new List<TicketNotification>();

    public virtual ICollection<TicketSlaTracking> TicketSlaTrackings { get; set; } = new List<TicketSlaTracking>();
}
