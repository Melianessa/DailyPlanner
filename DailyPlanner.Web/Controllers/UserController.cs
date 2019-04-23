using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository;
using Repository.Models;

namespace DailyPlanner.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        APIHelper _userAPI = new APIHelper();
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            List<UserDTO> user = new List<UserDTO>();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync("api/user/getAll");
                HttpResponseMessage resEvent = await client.GetAsync("api/event/getAll");
                if (res.IsSuccessStatusCode&& resEvent.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<List<UserDTO>>(result).OrderBy(p=>p.CreationDate).ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in GetAll method: {e.Message}");
            }

            return user;
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    List<User> users = new List<User>();
        //    try
        //    {
        //        HttpClient client = _userAPI.InitializeClient();
        //        HttpResponseMessage res = await client.GetAsync($"api/user/{id}");
        //        if (res.IsSuccessStatusCode)
        //        {
        //            var result = res.Content.ReadAsStringAsync().Result;
        //            users = JsonConvert.DeserializeObject<List<User>>(result);
        //        }
        //        var user = users.SingleOrDefault(m => m.Id == id);
        //        if (user == null)
        //        {
        //            _logger.LogWarning("Error in Get method, user is NULL");
        //            return NotFound();
        //        }
        //        return Ok(user);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogWarning($"Error in Get method: {e.Message}");
        //    }
        //    return Ok();
        //}
        [HttpPost]
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
                    HttpResponseMessage res = await client.PostAsync("api/user/post", content);
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
                HttpResponseMessage res = await client.GetAsync($"api/user/put/{id}");

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
                    HttpResponseMessage res = await client.PutAsync($"api/user/put/{id}", content);
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
                HttpResponseMessage res = await client.GetAsync($"api/user/delete/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
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
        }
}
