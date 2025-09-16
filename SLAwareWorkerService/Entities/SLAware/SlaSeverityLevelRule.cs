using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class SlaSeverityLevelRule
{
    public long Id { get; set; }

    public long SlaSeverityLevelId { get; set; }

    public long InitialResponseHours { get; set; }

    public long TargetResolutionHours { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual SlaSeverityLevel SlaSeverityLevel { get; set; } = null!;
}
