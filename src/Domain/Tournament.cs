using System;
using System.Collections.Generic;

namespace Domain
{
    public class NewTournamentWasCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }
    
    public class Tournament
    {
        public Tournament()
        {
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public List<Tuple<Guid, string>> Groups { get; set; }
    }
}