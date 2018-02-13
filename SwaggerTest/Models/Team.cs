using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerTest.Models
{
    public class Team
    {
        public Team(string name, string location)
        {
            this.Name = name;
            this.Location = location;
            this.Players = new List<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Player> Players { get; set; }
    }
}