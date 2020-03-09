using Domain.Util;

namespace Domain.GroupAggregate.DomainEvents
{
    public class NewTeamWasRegistered : IDomainEvent
    {
        public string TeamName { get; set; }

        public override string ToString()
        {
            return $"Team {TeamName} was added to the group";
        }
    }
}