using System;
using System.Threading.Tasks;
using Domain.GroupAggregate;
using Marten;

namespace Web.Queries
{
    public class GroupQueries
    {
        private readonly IDocumentSession _documentSession;

        public GroupQueries(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<Group> GetGroup(Guid id)
        {
            var group = await _documentSession.Events.AggregateStreamAsync<Group>(id);
            return group;
        }
        
    }
}