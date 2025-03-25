using AutoMapper;
using Calendar.Application.Contracts.Notes.Dto;
using Calendar.Domain.Notes;

namespace Calendar.Application
{
	public sealed class CalendarAutoMapperProfile : Profile
	{
		public CalendarAutoMapperProfile()
		{
			CreateMap<Note, NoteDto>();
			CreateMap<Note, NoteListDto>();
		}
	}
}