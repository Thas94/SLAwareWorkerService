using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAwareWorkerService.Entities.SLAware;
using SLAwareWorkerService.Interfaces;
using SLAwareWorkerService.Enums;

namespace SLAwareWorkerService.Services.SlaSeverity
{
    public class SlaSeverityService : SLAwareBaseService, ISlaSeverityService
    {
        public static readonly TimeSpan WorkStart = new TimeSpan(8, 30, 0);
        public static readonly TimeSpan WorkEnd = new TimeSpan(17, 0, 0);
        public SlaSeverityService(slaware_dataContext slaware_DataContext) : base(slaware_DataContext)
        { }

        public void SlaTracking()
        {
            try
            {
                var current = DateTime.Now;
                var workStartToday = current.Date.Add(WorkStart);
                var workEndToday = current.Date.Add(WorkEnd);

                if (!IsWorkingDay(DateTime.Now))
                {
                    var trackings = (from ticket in _slaware_DataContext.Tickets
                                    join track in _slaware_DataContext.TicketSlaTrackings on ticket.Id equals track.TicketId
                                    where ticket.TicketStatusId != (long)Enums.Enums.TicketStatus.Resolved 
                                    || ticket.TicketStatusId != (long)Enums.Enums.TicketStatus.Closed
                                    select new
                                    {
                                        ticket.Id,
                                        track.ResponseDueDtm,
                                        track.ResolutionDueDtm,
                                        track.RemainingResponseDueTime,
                                        track.RemainingResolutionDueTime,
                                        track.PausedDtm,
                                        track.IsResponseSlaBreach,
                                        track.IsResolutionSlaBreach
                                    }).ToList();

                    foreach (var track in trackings)
                    {
                        //Pause
                        if(current > workEndToday)
                        {
                            PauseSlaTimers(track.Id, workEndToday);
                        }

                        //Resume
                        if(current < workEndToday && track.PausedDtm.HasValue)
                        {
                            ResumeSlaTimers(track.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool IsWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday ? true : false;
        }

        private void PauseSlaTimers(long ticketId, DateTime workEndToday)
        {
            var sla = _slaware_DataContext.TicketSlaTrackings.Find(ticketId);
            sla.PausedDtm = DateTime.Now;
            sla.RemainingResponseDueTime = TimeOnly.FromTimeSpan(sla.ResponseDueDtm - workEndToday);
            sla.RemainingResolutionDueTime = TimeOnly.FromTimeSpan(sla.ResolutionDueDtm - workEndToday);
            //_slaware_DataContext.SaveChanges();
        }

        private void ResumeSlaTimers(long ticketId)
        {
            var current = DateTime.Now;
            var track = _slaware_DataContext.TicketSlaTrackings.Find(ticketId);
            track.ResponseDueDtm = current.Add(track.RemainingResponseDueTime.Value.ToTimeSpan());
            track.ResolutionDueDtm = current.Add(track.RemainingResolutionDueTime.Value.ToTimeSpan());
            track.PausedDtm = null;
            track.RemainingResponseDueTime = null;
            track.RemainingResolutionDueTime = null;
            //_slaware_DataContext.SaveChanges();
        }
    }
}
