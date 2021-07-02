using Newtonsoft.Json;
using NHLClient.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NHLClient.Events
{
    public class APIHandler
    {
        private readonly string _urlFragment = "/api/v1/";
        protected readonly string _baseAddress;
        protected readonly Client _client;

        public APIHandler(string baseAddress)
        {
            _client = new Client(baseAddress);

            _baseAddress = baseAddress;
        }

        /// <summary>
        /// Returns a list of data about all teams including their id, venue details, division, conference and franchise information.
        /// </summary>
        /// <param name="ids">The IDs of the teams to get info on. Pass in null to get all active teams.</param>
        /// <returns>An object containing the Copyright and a List of Teams.</returns>
        public async Task<Schema.Containers.Team> GetTeams(List<int> ids = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);            

            if (ids != null)
            {
                foreach (int id in ids) { query.Add("teamId", id.ToString()); }
            }

            HttpResponseMessage httpResponseMessage = await _client.GetAsync($"{_urlFragment}/teams/", $"?{query}", "");

            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Schema.Containers.Team>(httpResponseContent);
            }
            else
            {
                throw new HttpRequestException(httpResponseContent);
            }
        }
    }
}
