using System;
using Domain.Util;

namespace Domain.GroupAggregate
{
    public class ScheduledMatch : Entity
    {
        public ScheduledMatch(RegisteredTeam homeTeam, RegisteredTeam awayTeam, DateTime kickOff)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            KickOff = kickOff;
        }

        public RegisteredTeam HomeTeam { get; private set; }
        public RegisteredTeam AwayTeam { get; private set; }
        public DateTime KickOff { get; private set; }
    }
}