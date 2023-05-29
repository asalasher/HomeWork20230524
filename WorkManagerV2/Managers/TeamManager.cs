using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using WorkManagerV2;

namespace POOWorkersAdminV1
{
    public class TeamManager: ITeamManager
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

        public Team GetTeamByWorkerId(int idWorker)
        {
            foreach (var team in Teams)
            {
                if (team.IsWorkerInTeam(idWorker))
                {
                    return team;
                }
            }
            return null;
        }

        public bool DeleteIdWorkerFromTeam(int idWorker)
        {
            foreach (var team in Teams)
            {
                if (team.Manager.Id == idWorker)
                {
                    team.Manager = null;
                    return true;
                }
                else
                {
                    foreach (var worker in team.Technicians)
                    {
                        if (worker.Id == idWorker)
                        {
                            team.Technicians.Remove(worker);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}
