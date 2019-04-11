using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Repository;
using Repository.Models;

namespace DailyPlanner.Controllers
{
    public class APIHelper
    {
        private string _apiBaseURI = "http://localhost:64629";
        public HttpClient InitializeClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_apiBaseURI)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }

    public class UserHelper
    {
        public Guid EventId { get; set; }
        public User User { get; set; }
        public List<Event> UserEvent { get; set; }
    }
}
