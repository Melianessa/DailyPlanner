using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repository.Models;

namespace DailyPlanner.Web
{
    public class MapperClass : Profile
    {
        public MapperClass()
        {
            CreateMap<Event, User>();
        }
    }
}
