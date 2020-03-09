namespace Domain.GroupAggregate
{
    public class MatchResult
    {
        public MatchResult(ScheduledMatch match, MatchSign result)
        {
            Match = match;
            Result = result;
        }

        public ScheduledMatch Match { get; }
        public MatchSign Result { get; }
    }
}