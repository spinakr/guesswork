using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.GroupAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/tournaments")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;

        public TournamentController(ILogger<TournamentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IEnumerable<Tournament> Get()
        {
            return new List<Tournament>();
        }
        
        [HttpGet("{tournamentId}")]
        public Tournament Get(string tournamentId)
        {
            return new Tournament();
        }
        
        [HttpGet("{tournamentId}/groups")]
        public IEnumerable<Group> GetGroups(string tournamentId)
        {
            return new List<Group>();
        }
    }
}