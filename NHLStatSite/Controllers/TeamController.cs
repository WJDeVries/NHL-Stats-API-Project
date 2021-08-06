using System.Threading.Tasks;
using System.Web.Mvc;

namespace NHLStatSite.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        // GET: Team/Roster
        public ActionResult Roster(int id)
        {
            ViewBag.ID = id.ToString();

            return View();
        }

        public async Task<JsonResult> GetTeams()
        {
            NHLClient.Events.APIHandler api = new NHLClient.Events.APIHandler("https://statsapi.web.nhl.com");

            var teams = await api.GetTeams();

            return Json(teams);
        }

        public async Task<JsonResult> GetRoster(int teamId)
        {
            NHLClient.Events.APIHandler api = new NHLClient.Events.APIHandler("https://statsapi.web.nhl.com");

            var roster = await api.GetRoster(teamId);

            return Json(roster);
        }
    }
}