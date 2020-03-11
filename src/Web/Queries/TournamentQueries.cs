using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Marten;

namespace Web.Queries
{
    public class TournamentQueries
    {
        private readonly IDocumentSession _documentSession;

        public TournamentQueries(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<Tournament> GetTournament(Guid id)
        {
            return await _documentSession.Query<Tournament>().Where(t => t.Id == id).SingleAsync();
        }

        public async Task<IReadOnlyList<Tournament>> GetTournaments()
        {
            return await _documentSession.Query<Tournament>().ToListAsync();
        }
    }
}