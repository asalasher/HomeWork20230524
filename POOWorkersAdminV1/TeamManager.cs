using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace POOWorkersAdminV1
{
    internal class TeamManager
    {
        public List<Team> Teams { get; set; }

        public TeamManager(List<Team> teams)
        {
            Teams = teams;
        }

        public TeamManager()
        {
            Teams = new List<Team>();
        }

        public bool RegisterNewTeam(Team team)
        {
            Teams.Add(team);
            return true;
        }

        public Team GetTeamByName(string name)
        {
            foreach (var team in Teams)
            {
                if (team.Name == name)
                {
                    return team;
                }
            }
            return null;
        }
        public Team GetTeamById(int id)
        {
            foreach (var team in Teams)
            {
                if (team.Id == id)
                {
                    return team;
                }
            }
            return null;
        }

    }
}
