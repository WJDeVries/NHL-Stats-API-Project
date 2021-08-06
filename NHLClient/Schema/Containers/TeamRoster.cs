using System.Collections.Generic;

namespace NHLClient.Schema.Containers
{
    public class TeamRoster
    {
        public string Copyright { get; set; }
        public List<Player> Roster { get; set; }
    }
}
