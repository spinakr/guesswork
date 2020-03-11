using System;
using System.Collections.Generic;
using Domain;
using Domain.GroupAggregate.DomainEvents;
using Marten.Events.Projections;

namespace Web.Queries
{
    public class TournamentProjection : ViewProjection<Tournament, Guid>
    {
        public TournamentProjection()
        {
            ProjectEvent<NewTournamentWasCreated>(e => e.Id, UpdateTournament);
            ProjectEvent<NewGroupWasCreated>(e => e.TournamentId, UpdateTournament);
        }

        private static void UpdateTournament(Tournament model, NewTournamentWasCreated tournamentCreated)
        {
            model.Name = tournamentCreated.Name;
            model.Starts = tournamentCreated.Starts;
            model.Ends = tournamentCreated.Ends;
            model.Groups = new List<Tuple<Guid, string>>();
        }
        
        private static void UpdateTournament(Tournament model, NewGroupWasCreated groupCreated)
        {
            model.Groups.Add(new Tuple<Guid, string>(groupCreated.GroupId, groupCreated.GroupName));
        }
    }
}