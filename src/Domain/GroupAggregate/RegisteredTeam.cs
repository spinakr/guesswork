namespace Domain.GroupAggregate
{
    public class RegisteredTeam
    {
        public string Name { get; private set; }
        public int Points { get; private set; }
        public int Goals { get; private set; }
        
        public RegisteredTeam(string name, int points, int goals)
        {
            Name = name;
            Points = points;
            Goals = goals;
        }
        
        public static RegisteredTeam CreateNewTeam(string name) => new RegisteredTeam(name, 0, 0);
    }
}