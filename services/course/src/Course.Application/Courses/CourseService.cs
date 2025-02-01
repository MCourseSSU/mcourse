using AutoMapper;
using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Requests;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;

namespace Course.Application.Courses;

using Course.Domain.Courses;

internal sealed class CourseService : ICourseService
{
	private readonly ICourseRepository _courseRepository;
	private readonly IMapper _mapper;
	private readonly IGuidGenerator _guidGenerator;

	public CourseService(
		ICourseRepository courseRepository,
		IMapper mapper,
		IGuidGenerator guidGenerator)
	{
		_courseRepository = courseRepository;
		_mapper = mapper;
		_guidGenerator = guidGenerator;
	}

	public async Task<CourseDto> CreateCourseAsync(CreateCourseRequest request, CancellationToken cancellationToken)
	{
		var isExist = await _courseRepository.CheckCourseForExistenceAsync(request.Title, cancellationToken);

		if (isExist)
		{
			// TODO: Добавить результирующий объект
			return null;
		}

		var course = new Course(
			id: _guidGenerator.Create(),
			title: request.Title,
			description: request.Description);

		await _courseRepository.InsertAsync(
			entity: course,
			autoSave: true,
			cancellationToken: cancellationToken);

		// TODO: откинуть событие об уведомлении о успешном создании курса.

		return _mapper.Map<CourseDto>(course);
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