using AutoMapper;
using Student.DTOs;
using Student.Models;

namespace Student.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student.Models.Student, StudentDto>().ReverseMap();
            CreateMap<StudentCreateDto, Student.Models.Student>();
            CreateMap<StudentUpdateDto, Student.Models.Student>();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CourseCreateDto, Course>();
        }
    }
}