using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class Ticket
{
    public long Id { get; set; }

    public string TicketNumber { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long TicketStatusId { get; set; }

    public long SeverityLevelId { get; set; }

    public long CreatedById { get; set; }

    public long? AssignedToId { get; set; }

    public long SubCategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public long? CategoryId { get; set; }

    public virtual TicketCategory? Category { get; set; }

    public virtual SlaSeverityLevel SeverityLevel { get; set; } = null!;

    public virtual ICollection<TicketActivityLog> TicketActivityLogs { get; set; } = new List<TicketActivityLog>();

    public virtual ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();

    public virtual ICollection<TicketNotification> TicketNotifications { get; set; } = new List<TicketNotification>();

    public virtual ICollection<TicketSlaTracking> TicketSlaTrackings { get; set; } = new List<TicketSlaTracking>();
}
