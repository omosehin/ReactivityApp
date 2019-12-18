using AutoMapper;
using reactive.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Application.Activities
{
    public class MappingProfile :Profile //from automapper
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();  //from activity
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName));
        }
    }
}
