using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class TicketActivityLog
{
    public long Id { get; set; }

    public long TicketId { get; set; }

    public long UserId { get; set; }

    public string Description { get; set; } = null!;

    public long OldTicketStatusId { get; set; }

    public long? NewTicketStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual TicketStatus? NewTicketStatus { get; set; }

    public virtual TicketStatus OldTicketStatus { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
