using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DailyPlanner.DomainClasses.Models;
using DailyPlanner.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DailyPlanner.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [DailyPlannerExceptionFilter]
    public class EventController : Controller
    {
        APIHelper _userAPI = new APIHelper();
        private readonly ILogger _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get all Events to date.
        /// </summary>
        /// <param name="date">The event date to search for</param>
        /// <returns>A list of events</returns>
        [HttpPost]
        public async Task<IEnumerable<Event>> GetByDate(string date) //try [FromBody]
        {
            List<Event> ev = new List<Event>();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                //if (date == null)
                //{
                //    date = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
                //}
                var content = new StringContent(JsonConvert.SerializeObject(date), Encoding.UTF8, "application/json");
                // or JsonConvert.SerializeObject(date)
                HttpResponseMessage res = await client.PostAsync("api/event/getByDate", content);
                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    ev = JsonConvert.DeserializeObject<List<Event>>(result).OrderBy(p => p.StartDate).ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in GetByDate method: {e.Message}");
            }

            return ev;
        }
        //[HttpGet]
        //public async Task<IEnumerable<Event>> GetAll()
        //{
        //    List<Event> ev = new List<Event>();
        //    try
        //    {
        //        HttpClient client = _userAPI.InitializeClient();
        //        HttpResponseMessage res = await client.GetAsync("api/event/getAll");
        //        if (res.IsSuccessStatusCode)
        //        {
        //            var result = await res.Content.ReadAsStringAsync();
        //            ev = JsonConvert.DeserializeObject<List<Event>>(result).OrderBy(p => p.StartDate).ToList();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogWarning($"Error in GetAll method: {e.Message}");
        //    }

        //    return ev;
        //}
        /// <summary>
        /// Get a specific Event.
        /// </summary>
        /// <param name="id">The event id to search for</param>
        /// <returns>A event information</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            List<Event> evs = new List<Event>();
            try
            {
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/event/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    evs = JsonConvert.DeserializeObject<List<Event>>(result);
                }
                var ev = evs.SingleOrDefault(m => m.Id == id);
                if (ev == null)
                {
                    _logger.LogWarning("Error in Get method, event is NULL");
                    return NotFound();
                }

                return Ok(ev);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Get method: {e.Message}");
            }

            return Ok();
        }
        /// <summary>
        /// Creates a Event.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": "1",
        ///        "title": "birthday",
        ///        "description": "my birthday",
        ///        "startDate": "4/24/2019 1:00:00 PM",
        ///        "endDate": "4/25/2019 1:00:00 PM",
        ///        "creationDate": "4/24/2019 1:00:00 PM",
        ///        "isActive": true,
        ///        "type": 0,
        ///        "user": {
        ///            "id": "2",
        ///            "firstName": "Dan",
        ///            "lastName": "Reynolds",
        ///            "creationDate": "4/24/2019 1:00:00 PM",
        ///            "dateOfBirth": "7/14/1987 12:00:00 PM",
        ///            "phone": "5556677",
        ///            "email": "user@mail.com",
        ///            "sex": true,
        ///            "isActive": true,
        ///            "role": 0,
        ///            "events": [
        ///               null
        ///            ]
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="ev">An event to create</param>
        /// <returns>A newly created Event</returns>
        /// <response code="201">Returns the newly created event</response>
        /// <response code="400">If the event is null</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Event ev)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = _userAPI.InitializeClient();

                    var content = new StringContent(JsonConvert.SerializeObject(ev), Encoding.UTF8,
                        "application/json");
                    HttpResponseMessage res = await client.PostAsync("api/event/post", content);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetByDate");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Create method: {e.Message}");
            }

            return Ok(ev);
        }
        /// <summary>
        /// Edit a specific Event.
        /// </summary>
        /// <param name="id">The event id to edit for</param>
        [HttpGet("{id}")]
        public async Task<Event> Edit(Guid id)
        {
            Event ev = new Event();
            try
            {
                if (id == null)
                {
                    _logger.LogWarning("In Edit method Id is NULL");
                }

                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.GetAsync($"api/event/get/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    ev = JsonConvert.DeserializeObject<Event>(result);
                }
                if (ev == null)
                {
                    _logger.LogWarning("Error in Edit method, event is NULL");
                }
                return ev;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Edit method: {e.Message}");
            }
            return ev;
        }
        /// <summary>
        /// Edit a specific Event.
        /// </summary>
        /// <param name="ev">The event to edit for</param>
        [HttpPut("{id}")]
        public async Task<Event> Edit([FromBody]Event ev)
        {
            Event newEvent = new Event();
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = _userAPI.InitializeClient();

                    var content = new StringContent(JsonConvert.SerializeObject(ev), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PutAsync($"api/event/put/{ev.Id}", content);
                    if (res.IsSuccessStatusCode)
                    {
                        var result = await res.Content.ReadAsStringAsync();
                        newEvent = JsonConvert.DeserializeObject<Event>(result);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Edit method: {e.Message}");
            }
            return newEvent;
        }
        /// <summary>
        /// Deletes a specific Event.
        /// </summary>
        /// <param name="id">The event id to delete for</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Event ev = new Event();
                HttpClient client = _userAPI.InitializeClient();
                HttpResponseMessage res = await client.DeleteAsync($"api/event/delete/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    ev = JsonConvert.DeserializeObject<Event>(result);
                }
                if (ev == null)
                {
                    _logger.LogInformation("In Delete method user is NULL");
                    return NotFound();
                }
                return Ok(ev);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error in Delete method: {e.Message}");
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
        //        HttpResponseMessage res = client.DeleteAsync($"api/event/{id}").Result;
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