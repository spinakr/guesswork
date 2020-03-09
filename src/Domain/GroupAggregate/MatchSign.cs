using Domain.Util;

namespace Domain.GroupAggregate
{
    public class MatchSign : Enumeration
    {
        public static readonly MatchSign Home = new MatchSign(1, "H");
        public static readonly MatchSign Away = new MatchSign(2, "A");
        public static readonly MatchSign Draw = new MatchSign(0, "U");

        public MatchSign(int id, string name) : base(id, name)
        {
        }
    }
}