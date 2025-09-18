using System;
using System.Collections.Generic;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class TicketBreachLog
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? MessageTemplate { get; set; }

    public string? Level { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Exception { get; set; }

    public string? Properties { get; set; }

    public long? TicketId { get; set; }

    public DateTime? ResponseBreachedDate { get; set; }

    public DateTime? ResolutionBreachedDate { get; set; }

    public DateTime? ResponseDueDtm { get; set; }

    public DateTime? ResolutionDueDtm { get; set; }

    public TimeOnly? ResponseRemainingTime { get; set; }

    public TimeOnly? ResolutionRemainingTime { get; set; }
}
