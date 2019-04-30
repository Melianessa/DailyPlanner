using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyPlanner.DomainClasses;
using DailyPlanner.DomainClasses.Models;
using DailyPlanner.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        /// <summary>
        /// Get all Users.
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        [ServiceFilter(typeof(DailyPlannerExceptionFilterAttribute))]
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> user = new List<UserDTO>();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync("api/user/getAllUsers");
                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<List<UserDTO>>(result).OrderBy(p => p.CreationDate).ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in GetAllUsers method: {e.Message}");
            }

            return user;
        }
        //[HttpGet]
        //public async Task<IEnumerable<User>> GetAll()
        //{
        //    List<User> user = new List<User>();
        //    try
        //    {
        //        HttpClient client = _userAPI.InitializeClient();
        //        HttpResponseMessage res = await client.GetAsync("api/user/getAll");
        //        if (res.IsSuccessStatusCode)
        //        {
        //            var result = await res.Content.ReadAsStringAsync();
        //            user = JsonConvert.DeserializeObject<List<User>>(result).OrderBy(p=>p.CreationDate).ToList();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogWarning($"Error in GetAll method: {e.Message}");
        //    }

        //    return user;
        //}
        /// <summary>
        /// Get a specific User.
        /// </summary>
        /// <param name="id">The user id to search for</param>
        /// <returns>A user information</returns>
        [HttpGet("{id}")]
        public async Task<User> Get(Guid id)
        {
            User users = new User();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/user/get/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<User>(result);
                }
                if (users == null)
                {
                    _logger.LogWarning("Error in Get method, user is NULL");
                }
                return users;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Get method: {e.Message}");
            }
            return users;
        }
        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "firstName": "Dan",
        ///        "lastName": "Reynolds",
        ///        "creationDate": "4/24/2019 1:00:00 PM",
        ///        "dateOfBirth": "4/24/1996 12:00:00 PM",
        ///        "phone": 5556677,
        ///        "email": "user@mail.com",
        ///        "sex": true,
        ///        "isActive": true,
        ///        "role": 1,
        ///        "eventCount": 0
        ///     }
        ///
        /// </remarks>
        /// <param name="user">A user to create</param>
        /// <returns>A newly created User</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user is null</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody]User user)
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
                        return RedirectToAction("GetAllUsers");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Create method: {e.Message}");
            }

            return Ok(user);
        }
        /// <summary>
        /// Edit a specific User.
        /// </summary>
        /// <param name="id">The user id to edit for</param>
        [HttpGet("{id}")]
        public async Task<UserDTO> Edit(Guid id)
        {
            UserDTO user = new UserDTO();
            try
            {
                if (id == null)
                {
                    _logger.LogWarning("In Edit method Id is NULL");
                }
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/user/getUser/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDTO>(result);
                }

                return user;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Edit method: {e.Message}");
            }
            return user;
        }
        /// <summary>
        /// Edit a specific User.
        /// </summary>
        /// <param name="user">The user to edit for</param>
        [HttpPut("{id}")]
        public async Task<User> Edit([FromBody]User user)
        {
            User newUser = new User();
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = _userAPI.InitializeClient();

                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PutAsync($"api/user/put/{user.Id}", content);
                    if (res.IsSuccessStatusCode)
                    {
                        var result = await res.Content.ReadAsStringAsync();
                        newUser = JsonConvert.DeserializeObject<User>(result);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Edit method: {e.Message}");
            }
            return newUser;
        }
        /// <summary>
        /// Deletes a specific User.
        /// </summary>
        /// <param name="id">The user id to delete for</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                User user = new User();
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.DeleteAsync($"api/user/delete/{id}");

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
                _logger.LogWarning($"Error in Delete method: {e.Message}");
            }
            return Ok();
        }
    }
}
