using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagement.Application.DTOs.ManagedTask;
using TaskManagement.Application.DTOs.TaskType;
using TaskManagement.Domain;

namespace TaskManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskType, TaskTypeDto>().ReverseMap();
            CreateMap<ManagedTask, ManagedTaskDto>().ReverseMap();
            CreateMap<ManagedTask, CreateManagedTaskDto>().ReverseMap();
        }
    }
}
