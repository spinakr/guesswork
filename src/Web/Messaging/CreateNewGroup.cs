using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.GroupAggregate;
using Marten;
using NServiceBus;
using Web.Queries;

namespace Web.Messaging
{
    public class CreateNewGroup : ICommand
    {
        public string GroupName { get; set; }
        public Guid TournamentId { get; set; }
        public List<string> TeamNames { get; set; }
    }

    public class CreateNewGroupHandler : IHandleMessages<CreateNewGroup>
    {
        private readonly IDocumentSession _documentSession;

        public CreateNewGroupHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task Handle(CreateNewGroup message, IMessageHandlerContext context)
        {
            var tournament = await new TournamentQueries(_documentSession).GetTournament(message.TournamentId);

            if (tournament.Groups.Any(x => x.Item2 == message.GroupName)) 
                throw new Exception($"Group with name {message.GroupName} already exist in tournament {message.TournamentId}");
            
            var group = Group.CreateNewGroup(message.GroupName, message.TournamentId);
            foreach (var teamName in message.TeamNames)
            {
                group.AddNewTeam(teamName);
            }

            foreach (var domainEvent in group.DomainEvents)
            {
                _documentSession.Events.Append(group.Id, domainEvent);
            }

            await _documentSession.SaveChangesAsync();
        }
    }
}