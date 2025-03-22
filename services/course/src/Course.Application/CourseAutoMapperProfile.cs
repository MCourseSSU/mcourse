using AutoMapper;
using Course.Application.Contracts.Courses.Dto;

namespace Course.Application;

using Course.Domain.Courses;

public sealed class CourseAutoMapperProfile : Profile
{
	public CourseAutoMapperProfile()
	{
		CreateMap<Course, CourseListDto>();
		CreateMap<Course, CourseDto>();
		CreateMap<Chapter, ChapterDto>();
	}
}