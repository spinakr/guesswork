using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.GroupAggregate;
using Marten;
using NServiceBus;
using Web.Queries;

namespace Web.Messaging
{
    public class CreateNewTournament : ICommand
    {
        public Guid TournamentId { get; set; }
        public string Name { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }

    public class CreateNewTournamentHandler : IHandleMessages<CreateNewTournament>
    {
        private readonly IDocumentSession _documentSession;

        public CreateNewTournamentHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Task Handle(CreateNewTournament message, IMessageHandlerContext context)
        {
            _documentSession.Events.Append(message.TournamentId, new NewTournamentWasCreated
            {
                Id = message.TournamentId,
                Name = message.Name, 
                Starts = message.Starts, 
                Ends = message.Ends
            });
            return _documentSession.SaveChangesAsync();
        }
    }
}