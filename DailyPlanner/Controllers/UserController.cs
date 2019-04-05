using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository;

namespace DailyPlanner.Controllers
{
    public class UserController : Controller
    {
        APIHelper _userAPI = new APIHelper();
        public async Task<IActionResult> GetAll()
        {
            List<User> user = new List<User>();
            HttpClient client = _userAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/user");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<List<User>>(result);
            }

            return View(user);
        }
    }
}