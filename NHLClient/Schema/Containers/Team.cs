using System;
using System.Collections.Generic;
using System.Text;

namespace NHLClient.Schema.Containers
{
    public class Team
    {
        public string Copyright { get; set; }
        public List<Schema.Team> Teams { get; set; }
    }
}
