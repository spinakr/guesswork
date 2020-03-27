using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Web.Messaging;
using Web.Queries;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/tournaments")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly IMessageSession _messageSession;
        private readonly TournamentQueries _tournamentQueries;

        public TournamentController(ILogger<TournamentController> logger, IMessageSession messageSession, TournamentQueries tournamentQueries)
        {
            _logger = logger;
            _messageSession = messageSession;
            _tournamentQueries = tournamentQueries;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Tournament>> GetTournaments()
        {
            return await _tournamentQueries.GetTournaments();
        }
        
        [HttpGet("{tournamentId}")]
        public async Task<ActionResult<Tournament>> GetTournament(Guid tournamentId)
        {
            return await _tournamentQueries.GetTournament(tournamentId);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNewTournament(CreateTournamentRequest req)
        {
            var tournamentId = Guid.NewGuid();
            await _messageSession.SendLocal(new CreateNewTournament
            {
                TournamentId = tournamentId,
                Name = req.Name,
                Starts = req.Starts,
                Ends = req.Ends
            });
            return AcceptedAtAction(nameof(GetTournament), new {id = tournamentId});
        }
    }

    public class CreateTournamentRequest
    {
        public string Name { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }
}