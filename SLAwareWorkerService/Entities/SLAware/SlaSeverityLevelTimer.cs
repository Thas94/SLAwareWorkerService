using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class SlaSeverityLevelTimer
{
    public long Id { get; set; }

    public long SlaSeverityLevelId { get; set; }

    public bool Active { get; set; }

    public long InitialReponseHours { get; set; }

    public long TargetResolutionHours { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
