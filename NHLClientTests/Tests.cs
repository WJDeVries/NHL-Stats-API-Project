using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHLClientTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public async Task TeamGetTest()
        {
            var teamEvent = new NHLClient.Events.APIHandler("https://statsapi.web.nhl.com");

            List<int> ids = new List<int> { 17, 26 };

            var teamContainer = await teamEvent.GetTeams(ids);

            Assert.AreEqual(2, teamContainer.Teams.Count, "Wrong number of teams");

            Common.LayoutObject(teamContainer);

            var redWings = teamContainer.Teams.Find(t => t.Id == 17);
            var kings = teamContainer.Teams.Find(t => t.Id == 26);

            Assert.IsNotNull(redWings, "Could not find Red Wings");
            Assert.IsNotNull(kings, "Could not find Kings");

            Assert.AreEqual(17, redWings.Id, "Wrong Id");
            Assert.AreEqual("Detroit Red Wings", redWings.Name, "Wrong Name");
            Assert.AreEqual("/api/v1/teams/17", redWings.Link, "Wrong Link");
            Assert.AreEqual("DET", redWings.Abbreviation, "Wrong Abbreviation");
            Assert.AreEqual("Red Wings", redWings.TeamName, "Wrong TeamName");
            Assert.AreEqual("Detroit", redWings.LocationName, "Wrong LocationName");
            Assert.AreEqual("1926", redWings.FirstYearOfPlay, "Wrong FirstYearOfPlay");
            Assert.AreEqual("Detroit", redWings.ShortName, "Wrong ShortName");
            Assert.AreEqual("http://www.detroitredwings.com/", redWings.OfficialSiteUrl, "Wrong OfficialSiteUrl");
            Assert.AreEqual(12, redWings.FranchiseId, "Wrong FranchiseId");
            Assert.AreEqual(true, redWings.Active, "Wrong Active");

            Assert.IsNotNull(redWings.Venue, "Null Venue");
            Assert.IsNotNull(redWings.Division, "Null Division");
            Assert.IsNotNull(redWings.Conference, "Null Conference");
            Assert.IsNotNull(redWings.Franchise, "Null Franchise");

            NHLClient.Schema.Venue rwVenue = redWings.Venue;
            NHLClient.Schema.Division rwDivision = redWings.Division;
            NHLClient.Schema.Division rwConference = redWings.Conference;
            NHLClient.Schema.Franchise rwFranchise = redWings.Franchise;

            Assert.AreEqual(5145, rwVenue.Id, "Wrong Venue.Id");
            Assert.AreEqual("Little Caesars Arena", rwVenue.Name, "Wrong Venue.Name");
            Assert.AreEqual("/api/v1/venues/5145", rwVenue.Link, "Wrong Venue.Link");
            Assert.AreEqual("Detroit", rwVenue.City, "Wrong Venue.City");

            Assert.AreEqual(17, rwDivision.Id, "Wrong division.Id");
            Assert.AreEqual("Atlantic", rwDivision.Name, "Wrong division.Name");
            Assert.AreEqual("/api/v1/divisions/17", rwDivision.Link, "Wrong division.Link");

            Assert.AreEqual(6, rwConference.Id, "Wrong conference.Id");
            Assert.AreEqual("Eastern", rwConference.Name, "Wrong conference.Name");
            Assert.AreEqual("/api/v1/conferences/6", rwConference.Link, "Wrong conference.Link");

            Assert.AreEqual(12, rwFranchise.FranchiseId, "Wrong franchise.FranchiseId");
            Assert.AreEqual("Red Wings", rwFranchise.TeamName, "Wrong franchise.TeamName");
            Assert.AreEqual("/api/v1/franchises/12", rwFranchise.Link, "Wrong franchise.Link");

            Assert.AreEqual(26, kings.Id, "Wrong Id");
            Assert.AreEqual("Los Angeles Kings", kings.Name, "Wrong Name");
            Assert.AreEqual("/api/v1/teams/26", kings.Link, "Wrong Link");
            Assert.AreEqual("LAK", kings.Abbreviation, "Wrong Abbreviation");
            Assert.AreEqual("Kings", kings.TeamName, "Wrong TeamName");
            Assert.AreEqual("Los Angeles", kings.LocationName, "Wrong LocationName");
            Assert.AreEqual("1967", kings.FirstYearOfPlay, "Wrong FirstYearOfPlay");
            Assert.AreEqual("Los Angeles", kings.ShortName, "Wrong ShortName");
            Assert.AreEqual("http://www.lakings.com/", kings.OfficialSiteUrl, "Wrong OfficialSiteUrl");
            Assert.AreEqual(14, kings.FranchiseId, "Wrong FranchiseId");
            Assert.AreEqual(true, kings.Active, "Wrong Active");

            Assert.IsNotNull(kings.Venue, "Null Venue");
            Assert.IsNotNull(kings.Division, "Null Division");
            Assert.IsNotNull(kings.Conference, "Null Conference");
            Assert.IsNotNull(kings.Franchise, "Null Franchise");

            NHLClient.Schema.Venue kVenue = kings.Venue;
            NHLClient.Schema.Division kDivision = kings.Division;
            NHLClient.Schema.Division kConference = kings.Conference;
            NHLClient.Schema.Franchise kFranchise = kings.Franchise;

            Assert.AreEqual(5081, kVenue.Id, "Wrong Venue.Id");
            Assert.AreEqual("STAPLES Center", kVenue.Name, "Wrong Venue.Name");
            Assert.AreEqual("/api/v1/venues/5081", kVenue.Link, "Wrong Venue.Link");
            Assert.AreEqual("Los Angeles", kVenue.City, "Wrong Venue.City");

            Assert.AreEqual(15, kDivision.Id, "Wrong division.Id");
            Assert.AreEqual("Pacific", kDivision.Name, "Wrong division.Name");
            Assert.AreEqual("/api/v1/divisions/15", kDivision.Link, "Wrong division.Link");

            Assert.AreEqual(5, kConference.Id, "Wrong conference.Id");
            Assert.AreEqual("Western", kConference.Name, "Wrong conference.Name");
            Assert.AreEqual("/api/v1/conferences/5", kConference.Link, "Wrong conference.Link");

            Assert.AreEqual(14, kFranchise.FranchiseId, "Wrong franchise.FranchiseId");
            Assert.AreEqual("Kings", kFranchise.TeamName, "Wrong franchise.TeamName");
            Assert.AreEqual("/api/v1/franchises/14", kFranchise.Link, "Wrong franchise.Link");
        }

        [TestMethod]
        public async Task TeamGetAllTest()
        {
            var teamEvent = new NHLClient.Events.APIHandler("https://statsapi.web.nhl.com");

            var teamContainer = await teamEvent.GetTeams();

            Assert.AreEqual(32, teamContainer.Teams.Count, "Wrong number of teams");
        }

        [TestMethod]
        public async Task RosterGetTest()
        {
            var teamEvent = new NHLClient.Events.APIHandler("https://statsapi.web.nhl.com");

            var teamContainer = await teamEvent.GetRoster(17);

            Assert.AreEqual(21, teamContainer.Roster.Count, "Wrong number of players on the Red Wings roster");
        }
    }
}