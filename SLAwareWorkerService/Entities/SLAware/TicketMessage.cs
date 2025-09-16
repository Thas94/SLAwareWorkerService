using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class TicketMessage
{
    public long Id { get; set; }

    public long TicketId { get; set; }

    public string MessageContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? RepliedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
