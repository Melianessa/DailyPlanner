using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace DailyPlanner.Web
{
    public class APIHelper
    {
        //private string _apiBaseURI = "https://dailyplannerapi.azurewebsites.net";
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
    
    //public class UserHelper
    //{
    //    public Guid EventId { get; set; }
    //    public User User { get; set; }
    //    public List<Event> UserEvent { get; set; }
    //}
}
