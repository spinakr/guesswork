using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.GroupAggregate;
using Marten;
using Marten.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/tournaments")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly IMessageSession _messageSession;
        private readonly IDocumentSession _documentSession;

        public TournamentController(ILogger<TournamentController> logger, IMessageSession messageSession, IDocumentSession documentSession)
        {
            _logger = logger;
            _messageSession = messageSession;
            _documentSession = documentSession;
        }

        [HttpGet("")]
        public IEnumerable<Tournament> GetTournaments()
        {
            var tournaments = _documentSession.Query<Tournament>().ToList();
            return tournaments;
        }
        
        [HttpGet("{tournamentId}")]
        public Tournament GetTournament(string tournamentId)
        {
            return new Tournament();
        }
        
        [HttpGet("{tournamentId}/groups")]
        public IEnumerable<Group> GetGroups(string tournamentId)
        {
            return new List<Group>();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNewTournament(CreateTournamentRequest req)
        {
            var tournamentId = Guid.NewGuid();
            _documentSession.Events.Append(tournamentId, new NewTournamentWasCreated
            {
                Id = tournamentId,
                Name = req.Name, 
                Starts = req.Starts, 
                Ends = req.Ends
            });
            await _documentSession.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTournament), new {id = tournamentId});
        }
        
        
    }

    public class CreateTournamentRequest
    {
        public string Name { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }
}