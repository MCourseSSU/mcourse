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
	private readonly IClockService _clockService;
	public CourseService(
		ICourseRepository courseRepository,
		IMapper mapper,
		IGuidGenerator guidGenerator,
		IClockService clockService)
	{
		_courseRepository = courseRepository;
		_mapper = mapper;
		_guidGenerator = guidGenerator;
		_clockService = clockService;
	}

	public async Task<Result<CourseDto>> CreateAsync(CreateCourseRequest request, CancellationToken cancellationToken)
	{
		var isExist = await _courseRepository.CheckCourseForExistenceAsync(request.Title, cancellationToken);

		if (isExist)
		{
			return new Result<CourseDto>(errorMessage: $"Курс с таким названием уже существует");
		}

		var course = new Course(
			id: _guidGenerator.Create(),
			title: request.Title,
			creationTime: _clockService.Now(),
			creatorId: _guidGenerator.Create(), // TODO: Mock, удалить при создании сервиса авторизации
			description: request.Description);

		await _courseRepository.InsertAsync(
			entity: course,
			autoSave: true,
			cancellationToken: cancellationToken);

		// TODO: откинуть событие об уведомлении о успешном создании курса.

		return new Result<CourseDto>(
			data: _mapper.Map<CourseDto>(course));
	}

	public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var course = await _courseRepository.GetAsync(id, cancellationToken);

		if (course is null)
		{
			return new Result(errorMessage: "Курс не найден");
		}

		await _courseRepository.RemoveAsync(
			entity: course,
			autoSave: true, 
			cancellationToken: cancellationToken);

		return new Result();
	}

	public async Task<Result<CourseDto>> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		var course = await _courseRepository.GetAsync(id, cancellationToken);

		if (course is null )
		{
			return new Result<CourseDto>(errorMessage: "Курс не найден");
		}

		return new Result<CourseDto>(
			data: _mapper.Map<CourseDto>(course));
	}

	public async Task<Result<PagedResultDto<CourseListDto>>> GetListAsync(PagedListRequest request, CancellationToken cancellationToken)
	{
		var pagedResult = await _courseRepository.GetPagedListAsync(
			pageNumber: request.PageNumber,
			pageSize: request.PageSize,
			cancellationToken: cancellationToken);

		var courseListDto = _mapper.Map<IReadOnlyCollection<CourseListDto>>(pagedResult.Items);

		var pagedResultDto = new PagedResultDto<CourseListDto>
		{
			Items = courseListDto,
			TotalCount = pagedResult.TotalCount
		};

		return new Result<PagedResultDto<CourseListDto>>(data: pagedResultDto);
	}

	public async Task<Result<CourseDto>> UpdateAsync(UpdateCourseRequest request, CancellationToken cancellationToken)
	{
		var course = await _courseRepository.GetAsync(request.Id, cancellationToken);

		if (course is null)
		{
			return new Result<CourseDto>(errorMessage: $"Курс не найден");
		}

		course.Update(
			description: request.Description,
			updatedTime: _clockService.Now());

		await _courseRepository.UpdateAsync(
			entity: course,
			autoSave: true,
			cancellationToken: cancellationToken);

		return new Result<CourseDto>(
			data: _mapper.Map<CourseDto>(course));
	}
}