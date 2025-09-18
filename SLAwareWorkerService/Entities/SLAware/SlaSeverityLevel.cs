using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class SlaSeverityLevel
{
    public long Id { get; set; }

    public string Severity { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<SlaSeverityLevelRule> SlaSeverityLevelRules { get; set; } = new List<SlaSeverityLevelRule>();

    public virtual ICollection<SubCategorySeverityLevel> SubCategorySeverityLevels { get; set; } = new List<SubCategorySeverityLevel>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
