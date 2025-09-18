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
                        if (!track.IsResponseSlaBreach)
                        {
                            track.IsResponseSlaBreach = IsResponseBreached(track).Item1;
                            track.ResponseSlaBreachDtm = IsResponseBreached(track).Item2;
                        }
                        if (!track.IsResolutionSlaBreach)
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

                //if (IsWorkingDay(DateTime.Now))
                //{
                //    if (IsWorkingHours(DateTime.Now))
                //    {
                //        foreach (var track in _slaware_DataContext.TicketSlaTrackings.Where(x => x.ResponseDueDtm.Day == DateTime.Now.Day 
                //        || x.ResolutionDueDtm.Day == DateTime.Now.Day).ToList())
                //        {
                //            //Resume
                //            if (track.PausedDtm.HasValue)
                //                ResumeSlaTimers(track.TicketId);

                //            //check sla breach
                //            if(!track.IsResponseSlaBreach)
                //                track.IsResponseSlaBreach = IsResponseBreached(track);
                //            if(!track.IsResolutionSlaBreach)
                //                track.IsResolutionSlaBreach = IsResolutionBreached(track);
                //            _slaware_DataContext.SaveChanges();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void PauseSlaTimers(long ticketId, DateTime workEndToday)
        {
            var sla = _slaware_DataContext.TicketSlaTrackings.FirstOrDefault(x => x.TicketId == ticketId);
            sla.PausedDtm = workEndToday;
            if(!sla.IsResponseSlaBreach)
            {
                var test = WorkEnd - sla.CreatedAt.TimeOfDay;
                var level = _slaware_DataContext.SlaSeverityLevels.FirstOrDefault(x => x.Id == sla.SlaSeverityLevelId);
                var test2 = new TimeSpan((int)level.InitialReponseHours, 0, 0) - test;
                sla.RemainingResponseDueTime = TimeOnly.FromTimeSpan(test2);
            }
            if(!sla.IsResolutionSlaBreach)
            {
                var test =  WorkEnd - sla.CreatedAt.TimeOfDay;
                var level = _slaware_DataContext.SlaSeverityLevels.FirstOrDefault(x => x.Id == sla.SlaSeverityLevelId);
                var test2 = new TimeSpan((int)level.TargetResolutionHours, 0, 0) - test;
                sla.RemainingResolutionDueTime = TimeOnly.FromTimeSpan(test2);
            }
            _slaware_DataContext.SaveChanges();
        }

        private void ResumeSlaTimers(long ticketId)
        {
            var current = DateTime.Now;
            var track = _slaware_DataContext.TicketSlaTrackings.FirstOrDefault(x => x.TicketId == ticketId);
            if (!track.IsResponseSlaBreach && track.RemainingResponseDueTime.HasValue)
                track.ResponseDueDtm = current.Add(track.RemainingResponseDueTime.Value.ToTimeSpan());
            if (!track.IsResolutionSlaBreach && track.RemainingResolutionDueTime.HasValue)
                track.ResolutionDueDtm = current.Add(track.RemainingResolutionDueTime.Value.ToTimeSpan());
            track.PausedDtm = null;
            track.RemainingResponseDueTime = null;
            track.RemainingResolutionDueTime = null;
            _slaware_DataContext.SaveChanges();
            //track.ResponseDueDtm = CalculateSlaDue(track.CreatedAt, track.RemainingResponseDueTime.Value.ToTimeSpan());
            //track.ResolutionDueDtm = CalculateSlaDue(track.CreatedAt, track.RemainingResolutionDueTime.Value.ToTimeSpan());
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

        private DateTime CalculateSlaDue(DateTime start, TimeSpan slaDuration)
        {
            var current = start;
            var remaining = slaDuration;

            try
            {
                while (remaining > TimeSpan.Zero)
                {
                    //check and skip if current is weekends
                    if (!IsWorkingDay(current.Date))
                    {
                        current = current.Date.AddDays(1).Add(WorkStart);
                        continue;
                    }

                    var workStartToday = current.Date.Add(WorkStart);
                    var workEndToday = current.Date.Add(WorkEnd);

                    var availableToday = workEndToday - current;
                    var timeToAdd = remaining < availableToday ? remaining : availableToday;

                    current = current.Add(timeToAdd);
                    remaining -= timeToAdd;

                    //check if there's hours remaining, move to the next day
                    if (remaining > TimeSpan.Zero)
                        current = current.Date.AddDays(1).Add(WorkStart);
                }
            }
            catch (Exception ex)
            {

            }
            return current;
        }

    }
}
