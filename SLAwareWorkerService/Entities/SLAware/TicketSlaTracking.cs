using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class TicketSlaTracking
{
    public long Id { get; set; }

    public long TicketId { get; set; }

    public long SlaSeverityLevelId { get; set; }

    public DateTime ResponseDueDtm { get; set; }

    public DateTime ResolutionDueDtm { get; set; }

    public DateTime? FirstResponseAt { get; set; }

    public TimeOnly? RemainingResponseDueTime { get; set; }

    public TimeOnly? RemainingResolutionDueTime { get; set; }

    public DateTime? PausedDtm { get; set; }

    public DateTime? ResolvedDtm { get; set; }

    public bool IsResponseSlaBreach { get; set; }

    public bool IsResolutionSlaBreach { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ResponseSlaBreachDtm { get; set; }

    public DateTime? ResolutionSlaBreachDtm { get; set; }

    public virtual SlaSeverityLevel SlaSeverityLevel { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
