using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class ClientTier
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<ClientTierCommunicationType> ClientTierCommunicationTypes { get; set; } = new List<ClientTierCommunicationType>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
