﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DailyPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository;
using Repository.Models;

namespace DailyPlanner.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        APIHelper _userAPI = new APIHelper();
        private readonly ILogger _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<User>> GetAll()
        {
            List<User> user = new List<User>();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync("api/user");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<List<User>>(result).OrderBy(p=>p.CreationDate).ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in GetAll method: {e.Message}");
            }

            return user;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get(Guid id)
        {
            List<User> users = new List<User>();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/user/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
                var user = users.SingleOrDefault(m => m.Id == id);
                if (user == null)
                {
                    _logger.LogWarning("Error in Get method, user is NULL");
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Get method: {e.Message}");
            }
            return Ok();
        }
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,FirstName,LastName,CreationDate,DateOfBirth,Phone,Email,Sex,IsActive,Role")]
            User user)
        {
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                if (ModelState.IsValid)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
                        "application/json");
                    HttpResponseMessage res = await client.PostAsync("api/user", content);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Create method: {e.Message}");
            }

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogWarning("In Edit method Id is NULL");
                    return NotFound();
                }

                List<User> users = new List<User>();
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/user/{id}");

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

                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Edit method: {e.Message}");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,FirstName,LastName,CreationDate,DateOfBirth,Phone,Email,Sex,IsActive,Role")]
            User user)
        {
            try
            {
                if (id != user.Id)
                {
                    _logger.LogInformation("In Edit method IDs don't match");
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    HttpClient client = _userAPI.InitializeClient();

                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PutAsync($"api/user/{id}", content);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Edit method: {e.Message}");
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogInformation("In Delete method Id is NULL");
                    return NotFound();
                }
                User user = new User();
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/user/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(result);
                }
                if (user == null)
                {
                    _logger.LogInformation("In Delete method user is NULL");
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Delte method: {e.Message}");
            }
            return Ok();
        }

        //[HttpDelete]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(Guid id)
        //{
        //    try
        //    {
        //        HttpClient client = _userAPI.InitializeClient();
        //        HttpResponseMessage res = client.DeleteAsync($"api/user/{id}").Result;
        //        if (res.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetAll");
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogWarning(e.Message);
        //    }
        //    return Ok();
        //}
    }
}