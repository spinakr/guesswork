using Domain.Util;

namespace Domain.GroupAggregate.DomainEvents
{
    public class NewGroupWasCreated : IDomainEvent
    {
        public string TournamentName { get; set; }
        public string GroupName { get; set; }

        public override string ToString()
        {
            return $"New group {GroupName} was added to tournament {TournamentName}";
        }
    }
}