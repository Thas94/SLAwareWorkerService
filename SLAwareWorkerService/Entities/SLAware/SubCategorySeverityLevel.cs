using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class SubCategorySeverityLevel
{
    public int Id { get; set; }

    public long SubCategoryId { get; set; }

    public long SlaSeverityLevelId { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SlaSeverityLevel SlaSeverityLevel { get; set; } = null!;

    public virtual TicketSubCategory SubCategory { get; set; } = null!;
}
