using AutoMapper;
using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Requests;
using Course.Domain.Courses;
using Shared.Contracts.Contracts.Dto;

namespace Course.Application.Courses
{
	internal sealed class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;
		private readonly IMapper _mapper;

		public CourseService(
			ICourseRepository courseRepository,
			IMapper mapper)
		{
			_courseRepository = courseRepository;
			_mapper = mapper;
		}

		public async Task<PagedResultDto<CourseListDto>> GetListAsync(PagedListRequest request, CancellationToken cancellationToken)
		{
			var pagedResult = await _courseRepository.GetPagedListAsync(
				pageNumber: request.PageNumber,
				pageSize: request.PageSize,
				cancellationToken: cancellationToken);

			var courseListDto = _mapper.Map<IReadOnlyCollection<CourseListDto>>(pagedResult.Items);

			return new PagedResultDto<CourseListDto>
			{
				Items = courseListDto,
				TotalCount = pagedResult.TotalCount
			};
		}
	}
}