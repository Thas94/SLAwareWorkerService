using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<TicketActivityLog> TicketActivityLogs { get; set; } = new List<TicketActivityLog>();

    public virtual ICollection<TicketNotification> TicketNotifications { get; set; } = new List<TicketNotification>();

    public virtual ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();
}
