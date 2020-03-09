using System;
using System.Collections.Generic;

namespace Domain
{
    public class Tournament
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string> GroupIds { get; set; }
    }
}