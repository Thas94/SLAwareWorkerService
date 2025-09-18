using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class TicketSubCategory
{
    public long Id { get; set; }

    public long TicketCategoryId { get; set; }

    public long? TagId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<SubCategorySeverityLevel> SubCategorySeverityLevels { get; set; } = new List<SubCategorySeverityLevel>();

    public virtual TicketCategory TicketCategory { get; set; } = null!;
}
