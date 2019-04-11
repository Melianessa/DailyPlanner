using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        public async Task<IActionResult> Get(Guid id)
        {
            List<User> user = new List<User>();
            HttpClient client = _userAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync($"api/user/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<List<User>>(result);
            }

            return View(user);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,CreationDate,DateOfBirth,Phone,Email,Sex,IsActive,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _userAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/user", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View(user);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<User> users = new List<User>();
            HttpClient client = _userAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/user");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(result);
            }

            var user = users.SingleOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,FirstName,LastName,CreationDate,DateOfBirth,Phone,Email,Sex,IsActive,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpClient client = _userAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync($"api/user/{id}", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<User> users = new List<User>();
            HttpClient client = _userAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/user");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(result);
            }

            var user = users.SingleOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            HttpClient client = _userAPI.InitializeClient();
            HttpResponseMessage res = client.DeleteAsync($"api/user/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }

            return NotFound();
        }
    }
}