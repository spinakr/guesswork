using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.GroupAggregate;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Web.Messaging;
using Web.Queries;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IMessageSession _messageSession;
        private readonly IDocumentSession _documentSession;

        public GroupsController(ILogger<GroupsController> logger, IMessageSession messageSession, IDocumentSession documentSession)
        {
            _logger = logger;
            _messageSession = messageSession;
            _documentSession = documentSession;
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<Group>> Get(Guid groupId)
        {
            var groupQueries = new GroupQueries(_documentSession);
            var group = await groupQueries.GetGroup(groupId);
            return group;
        }
        
        [HttpPost]
        public async Task<ActionResult<Group>> CreateNewGroup(CreateGroupRequest req)
        {
            await _messageSession.SendLocal(new CreateNewGroup
            {
                GroupName = req.GroupName,
                TournamentId = req.TournamentId,
                TeamNames = req.TeamNames
            });

            return Accepted();
        }
        
    }

    public class CreateGroupRequest
    {
        public Guid TournamentId { get; set; }
        public string GroupName { get; set; }
        public List<string> TeamNames { get; set; }
    }
}