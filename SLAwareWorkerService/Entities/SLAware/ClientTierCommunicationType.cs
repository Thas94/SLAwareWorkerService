using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class ClientTierCommunicationType
{
    public long Id { get; set; }

    public long ClientTierId { get; set; }

    public long CommunicationTypeId { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ClientTier ClientTier { get; set; } = null!;

    public virtual CommunicationType CommunicationType { get; set; } = null!;
}
