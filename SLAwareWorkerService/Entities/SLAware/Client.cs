using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class Client
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ClientTierId { get; set; }

    public virtual ClientTier ClientTier { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
