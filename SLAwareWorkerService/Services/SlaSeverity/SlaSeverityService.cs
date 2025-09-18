using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAwareWorkerService.Entities.SLAware;
using SLAwareWorkerService.Interfaces;
using SLAwareWorkerService.Enums;
using Microsoft.EntityFrameworkCore;

namespace SLAwareWorkerService.Services.SlaSeverity
{
    public class SlaSeverityService : SLAwareBaseService, ISlaSeverityService
    {
        public SlaSeverityService(slaware_dataContext slaware_DataContext) : base(slaware_DataContext)
        { }

        public void SlaTracking()
        {
            try
            {
                var current = DateTime.Now;
                var workStartToday = current.Date.Add(WorkStart);
                var workEndToday = current.Date.Add(WorkEnd);

                foreach (var track in _slaware_DataContext.TicketSlaTrackings.Where(x => x.ResponseDueDtm.Day == DateTime.Now.Day
                        || x.ResolutionDueDtm.Day == DateTime.Now.Day).ToList())
                {
                    if (IsWorkingDay(DateTime.Now) && IsWorkingHours(DateTime.Now))
                    {
                        //Resume
                        if (track.PausedDtm.HasValue)
                            ResumeSlaTimers(track.TicketId);

                        //check sla breach
                        if (track.IsResponseSlaBreach == false)
                        {
                            track.IsResponseSlaBreach = IsResponseBreached(track).Item1;
                            track.ResponseSlaBreachDtm = IsResponseBreached(track).Item2;
                        }
                        if (!track.IsResolutionSlaBreach == false)
                        {
                            track.IsResolutionSlaBreach = IsResolutionBreached(track).Item1;
                            track.ResolutionSlaBreachDtm = IsResolutionBreached(track).Item2;
                        }
                        _slaware_DataContext.SaveChanges();
                    }
                    else
                    {
                        //Pause
                        if (!track.PausedDtm.HasValue)
                            PauseSlaTimers(track.TicketId, workEndToday);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void PauseSlaTimers(long ticketId, DateTime workEndToday)
        {
            var track = _slaware_DataContext.TicketSlaTrackings.FirstOrDefault(x => x.TicketId == ticketId);
            if(track != null)
            {
                track.PausedDtm = workEndToday;
                if (track.IsResponseSlaBreach == false)
                {
                    if (track.ResponseDueDtm.Day == DateTime.Now.Day)
                    {
                        var remaining = track.ResponseDueDtm - DateTime.Now.Date.Add(WorkEnd);
                        if(track.RemainingResponseDueTime.HasValue)
                        {
                            remaining = track.RemainingResponseDueTime.Value.ToTimeSpan() - remaining;
                        }
                        track.RemainingResponseDueTime = TimeOnly.FromTimeSpan(remaining);
                    }
                }
                if (track.IsResolutionSlaBreach == false)
                {
                    if (track.ResolutionDueDtm.Day == DateTime.Now.Day)
                    {
                        var remaining = track.ResolutionDueDtm - DateTime.Now.Date.Add(WorkEnd);
                        if (track.RemainingResolutionDueTime.HasValue)
                        {
                            remaining = track.RemainingResolutionDueTime.Value.ToTimeSpan() - remaining;
                        }
                        track.RemainingResolutionDueTime = TimeOnly.FromTimeSpan(remaining);
                    }
                }
                _slaware_DataContext.SaveChanges();
            }
        }

        private void ResumeSlaTimers(long ticketId)
        {
            var current = DateTime.Now;
            var track = _slaware_DataContext.TicketSlaTrackings.FirstOrDefault(x => x.TicketId == ticketId);
            if(track != null)
            {
                if (track.IsResponseSlaBreach == false && track.RemainingResponseDueTime.HasValue)
                    track.ResponseDueDtm = current.Add(track.RemainingResponseDueTime.Value.ToTimeSpan());
                if (track.IsResolutionSlaBreach == false && track.RemainingResolutionDueTime.HasValue)
                    track.ResolutionDueDtm = current.Add(track.RemainingResolutionDueTime.Value.ToTimeSpan());
                track.PausedDtm = null;
                track.RemainingResponseDueTime = null;
                track.RemainingResolutionDueTime = null;
                _slaware_DataContext.SaveChanges();
            }
        }

        private (bool, DateTime?) IsResponseBreached(TicketSlaTracking ticket)
        {
            if (ticket.FirstResponseAt == null && DateTime.Now > ticket.ResponseDueDtm)
                return (true, DateTime.Now);

            if (ticket.FirstResponseAt != null && ticket.FirstResponseAt > ticket.ResponseDueDtm)
                return (true, DateTime.Now);

            return (false, null);
        }

        private (bool, DateTime?) IsResolutionBreached(TicketSlaTracking ticket)
        {
            if (ticket.ResolvedDtm == null && DateTime.Now > ticket.ResolutionDueDtm)
                return (true, DateTime.Now);

            if (ticket.ResolvedDtm != null && ticket.ResolvedDtm > ticket.ResolutionDueDtm)
                return (true, DateTime.Now);

            return (false, null);
        }

        
    }
}
