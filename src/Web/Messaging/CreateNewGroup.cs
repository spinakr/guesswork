using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.GroupAggregate;
using Marten;
using NServiceBus;

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

        public Task Handle(CreateNewGroup message, IMessageHandlerContext context)
        {
            var group = Group.CreateNewGroup(message.GroupName, message.TournamentId);
            foreach (var teamName in message.TeamNames)
            {
                group.AddNewTeam(teamName);
            }

            _documentSession.Events.Append(group.Id, group.DomainEvents);

            return Task.CompletedTask;
        }
    }
}