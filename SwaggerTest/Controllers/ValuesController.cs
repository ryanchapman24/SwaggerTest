using Newtonsoft.Json;
using SwaggerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwaggerTest.Controllers
{
    class Environment
    {
        public List<Team> _teams;
        private int _nextTeamId;

        public Environment()
        {
            this._teams = new List<Team>();
            this._nextTeamId = 0;

            Team t1 = new Team("Carolina Panthers", "Charlotte, NC");
            Team t2 = new Team("New Orleans Saints", "New Orleans, LA");
            Team t3 = new Team("Atlanta Falcons", "Atlanta, GA");
            Team t4 = new Team("Tampa Bay Buccaneers", "Tampa, FL");

            this.AddTeam(t1);
            this.AddTeam(t2);
            this.AddTeam(t3);
            this.AddTeam(t4);

            Player[] players = {
            new Player { Number=1, TeamId=1, FirstName="Cam", LastName="Newton", Age=28, Position="QB" },
                new Player { Number=59, TeamId=1, FirstName="Luke", LastName="Kuechly", Age=26, Position="LB" },
                new Player { Number=88, TeamId=1, FirstName="Greg", LastName="Olsen", Age=32, Position="TE" },
                new Player { Number=90, TeamId=1, FirstName="Julius", LastName="Peppers", Age=38, Position="DE" },
                new Player { Number=9, TeamId=2, FirstName="Drew", LastName="Brees", Age=39, Position="QB" },
                new Player { Number=13, TeamId=2, FirstName="Michael", LastName="Thomas", Age=24, Position="WR" },
                new Player { Number=94, TeamId=2, FirstName="Cameron", LastName="Jordan", Age=28, Position="DE" },
                new Player { Number=41, TeamId=2, FirstName="Alvin", LastName="Kamara", Age=22, Position="RB" },
                new Player { Number=2, TeamId=3, FirstName="Matt", LastName="Ryan", Age=32, Position="QB" },
                new Player { Number=11, TeamId=3, FirstName="Julio", LastName="Jones", Age=28, Position="WR" },
                new Player { Number=24, TeamId=3, FirstName="Devonta", LastName="Freeman", Age=25, Position="RB" },
                new Player { Number=44, TeamId=3, FirstName="Vic", LastName="Beasley", Age=25, Position="DE" },
                new Player { Number=3, TeamId=4, FirstName="Jameis", LastName="Winston", Age=24, Position="QB" },
                new Player { Number=13, TeamId=4, FirstName="Mike", LastName="Evans", Age=24, Position="WR" },
                new Player { Number=22, TeamId=4, FirstName="Doug", LastName="Martin", Age=29, Position="RB" },
                new Player { Number=93, TeamId=4, FirstName="Gerald", LastName="McCoy", Age=29, Position="DT" }
            };

            foreach (var team in this._teams)
            {
                for (var i = 0; i < players.Length; i++)
                {
                    if (players[i].TeamId == team.Id)
                    {
                        team.Players.Add(players[i]);
                    }
                }
            }
        }

        public void AddTeam(Team team)
        {
            team.Id = ++this._nextTeamId;
            this._teams.Add(team);
        }
    }
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        Environment env = new Environment();

        // GET api/teams
        /// <summary>
        /// Returns all teams ordered alphabetically by location as well as listing their respective players
        /// </summary>
        /// <returns></returns>
        [Route("Teams")]
        public IEnumerable<Team> GetTeams()
        {
            return env._teams.OrderBy(t => t.Location);
        }

        // GET api/team/5
        /// <summary>
        /// Return the team associated with the ID provided along with all of its players
        /// </summary>
        /// <param name="id">Required</param>
        /// <returns></returns>
        [Route("Team/{id}")]
        public Team GetTeam(int id)
        {
            return env._teams.First(t => t.Id == id);
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // GET api/jerseyRange/5/5
        /// <summary>
        /// Returns all players within the jersey range provided, ordered ascending by number
        /// </summary>
        /// <param name="lowNumber">Required</param>
        /// <param name="highNumber">Required</param>
        /// <returns></returns>
        [Route("JerseyRange/{lowNumber}-{highNumber}")]
        [AcceptVerbs("GET")]
        public IEnumerable<Player> JerseyRange(int lowNumber, int highNumber)
        {
            return env._teams.SelectMany(t => t.Players.Where(p => p.Number >= lowNumber && p.Number <= highNumber)).OrderBy(p => p.Number);
        }

        // DELETE api/values/5
        /// <summary>
        /// Deletes a particular team from the list
        /// </summary>
        /// <param name="id">Required</param>
        /// <returns></returns>
        [Route("DeleteTeam/{id}")]
        public string Delete(int id)
        {
            if (env._teams.Any(t => t.Id == id)){
                var teamToDelete = env._teams.First(t => t.Id == id);
                env._teams.Remove(teamToDelete);
                return teamToDelete.Name + " were removed from the list of teams.";
            }
            else
            {
                return "Failure";
            }
        }
    }
}
