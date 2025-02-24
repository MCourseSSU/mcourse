using AutoMapper;
using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Dto;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;
using Course.EntityFrameworkCore.Courses;
using Course.Application.Contracts.Courses.Commands;
using System.Data;

namespace Course.Application.Courses;

using Course.Domain.Courses;

internal sealed class CourseService : ICourseService
{
	private readonly ICourseRepository _courseRepository;
	private readonly IMapper _mapper;
	private readonly IGuidGenerator _guidGenerator;
	private readonly IClockService _clockService;
	private readonly IUnitOfWork _unitOfWork;
	
	public CourseService(
		ICourseRepository courseRepository,
		IMapper mapper,
		IGuidGenerator guidGenerator,
		IClockService clockService,
		IUnitOfWork unitOfWork)
	{
		_courseRepository = courseRepository;
		_mapper = mapper;
		_guidGenerator = guidGenerator;
		_clockService = clockService;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<CourseDto>> CreateAsync(CreateCourseCommand command, CancellationToken cancellationToken)
	{
		var isExist = await _courseRepository.CheckCourseForExistenceAsync(command.Title, cancellationToken);

		if (isExist)
		{
			return new Result<CourseDto>(errorMessage: $"Курс с таким названием уже существует");
		}

		var course = new Course(
			id: _guidGenerator.Create(),
			title: command.Title,
			creationTime: _clockService.Now(),
			creatorId: _guidGenerator.Create(), // TODO: Mock, удалить при создании сервиса авторизации
			description: command.Description);

		foreach (var item in command.Chapters)
		{
			var chapter = new Chapter(
				id: _guidGenerator.Create(),
				courseId: course.Id,
				title: item.Title);

			course.AddChapter(chapter);
		}

		try
		{
			await _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

			await _courseRepository.InsertAsync(
				entity: course,
				cancellationToken: cancellationToken);

			await _unitOfWork.CommitAsync(cancellationToken);
		}
		catch (Exception ex)
		{
			// TODO: Добавить логирование.
			await _unitOfWork.RollbackAsync(cancellationToken);
			throw;
		}

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

	public async Task<Result<PagedResultDto<CourseListDto>>> GetListAsync(PagedListCommand request, CancellationToken cancellationToken)
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

	public async Task<Result<CourseDto>> UpdateAsync(UpdateCourseCommand request, CancellationToken cancellationToken)
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