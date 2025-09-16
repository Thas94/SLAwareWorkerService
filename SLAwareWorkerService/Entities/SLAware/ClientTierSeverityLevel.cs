using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class ClientTierSeverityLevel
{
    public int Id { get; set; }

    public long ClientTierId { get; set; }

    public long SlaSeverityLevelId { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;
}
