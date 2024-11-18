using UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetAll;

namespace UnitTest.ApplicationServices.ServiceGroups;

public class GetAllServiceGroupQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetAllServiceGroupQueryHandler _handler;

    public GetAllServiceGroupQueryHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new GetAllServiceGroupQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenGroupsExist_ShouldReturnPagedResults()
    {
        // Arrange
        var request = new GetAllServiceGroupQueryRequest(
            new PaginationFilter(1, 10),
            null, // فیلترینگ نمونه
            null  // ترتیب‌دهی نمونه
        );

        // داده‌های تست
        var sectionGroups = new List<SectionGroup>
        {
            new SectionGroup { Id = 1, Name = "Group 1", Type = SectionType.Service },
            new SectionGroup { Id = 2, Name = "Group 2", Type = SectionType.Service },
        };

        // شبیه‌سازی پیدا کردن گروه‌ها از دیتابیس
        _uow.SectionGroups.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<object?>(), Arg.Any<object?>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(new ListDto(
                sectionGroups.Count,
                sectionGroups.Select(s => s.Adapt<SectionGroupDto>())
            ));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalRecords.Should().Be(2);
    }

    [Fact]
    public async Task Handle_WhenNoGroupsExist_ShouldReturnEmptyResults()
    {
        // Arrange
        var request = new GetAllServiceGroupQueryRequest(
            new PaginationFilter(1, 10),
            null,
            null
        );

        _uow.SectionGroups.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<object?>(), Arg.Any<object?>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(new ListDto(0, []));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEmpty();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalRecords.Should().Be(0);
    }

    [Fact]
    public async Task Handle_WhenApplyFilters_ShouldReturnFilteredResults()
    {
        // Arrange
        var request = new GetAllServiceGroupQueryRequest(
            new PaginationFilter(1, 10),
            new ServiceGroupFiltring("Group 1"),
            null
        );

        var sectionGroups = new List<SectionGroup>
        {
            new SectionGroup { Id = 1, Name = "Group 1", Type = SectionType.Service },
            new SectionGroup { Id = 2, Name = "Group 2", Type = SectionType.Service },
        };

        _uow.SectionGroups.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<object?>(), Arg.Any<object?>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(new ListDto(
                sectionGroups.Count,
                sectionGroups.Where(s => s.Name == "Group 1").Select(s => s.Adapt<SectionGroupDto>())
            ));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data.First().Name.Should().Be("Group 1");
    }
}