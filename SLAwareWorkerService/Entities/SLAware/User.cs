using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class User
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public long ClientTierId { get; set; }

    public virtual ClientTier ClientTier { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TicketActivityLog> TicketActivityLogs { get; set; } = new List<TicketActivityLog>();

    public virtual ICollection<TicketNotification> TicketNotifications { get; set; } = new List<TicketNotification>();
}
