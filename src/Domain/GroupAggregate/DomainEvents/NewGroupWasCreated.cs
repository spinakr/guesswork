using System;
using Domain.Util;

namespace Domain.GroupAggregate.DomainEvents
{
    public class NewGroupWasCreated : IDomainEvent
    {
        public Guid TournamentId { get; set; }
        public string GroupName { get; set; }
        public Guid GroupId { get; set; }

        public override string ToString()
        {
            return $"New group {GroupName} was added to tournament {TournamentId}";
        }
    }
}