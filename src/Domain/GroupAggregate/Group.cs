using System;
using System.Collections.Generic;
using Domain.GroupAggregate.DomainEvents;
using Domain.Util;

namespace Domain.GroupAggregate
{
    public class Group : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public List<RegisteredTeam> Teams { get; private set; }
        public List<ScheduledMatch> Fixtures { get; private set; }

        public static Group CreateNewGroup(string name, Guid tournamentId)
        {
            var group = new Group();
            group.AddDomainEvent(new NewGroupWasCreated {GroupId = Guid.NewGuid(), GroupName = name, TournamentId = tournamentId});
            return group;
        }

        public void AddNewTeam(string teamName)
        {
            AddDomainEvent(new NewTeamWasRegistered {TeamName = teamName});
        }

        public void Apply(NewGroupWasCreated groupCreated)
        {
            Id = groupCreated.GroupId;
            Name = groupCreated.GroupName;
            Teams = new List<RegisteredTeam>();
        }

        public void Apply(NewTeamWasRegistered teamRegistered)
        {
            Teams.Add(RegisteredTeam.CreateNewTeam(teamRegistered.TeamName));
        }
    }
}