namespace UnitTest.ApplicationServices.MenuGroups;

public class GetAllMenuGroupQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetAllMenuGroupQueryHandler _handler;

    public GetAllMenuGroupQueryHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new GetAllMenuGroupQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenMenuGroupsExist_ShouldReturnMenuGroups()
    {
        // Arrange
        var request = new GetAllMenuGroupQueryRequest(
            new PaginationFilter(1, 10), 
            null, 
            null
        );

        var sectionGroups = new List<SectionGroup>
        {
            new SectionGroup { Id = 1, Name = "Menu Group 1", Type = SectionType.Menu },
            new SectionGroup { Id = 2, Name = "Menu Group 2", Type = SectionType.Menu }
        };

        _uow.SectionGroups.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<object?>(), Arg.Any<object?>(), SectionType.Menu, Arg.Any<CancellationToken>())
            .Returns(new ListDto(sectionGroups.Count, sectionGroups.Select(x => x.Adapt<SectionGroupDto>())));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Data.Should().HaveCount(2);
        result.Data.First().Name.Should().Be("Menu Group 1");
        result.Data.Last().Name.Should().Be("Menu Group 2");
    }

    [Fact]
    public async Task Handle_WhenNoMenuGroupsExist_ShouldReturnEmptyList()
    {
        // Arrange
        var request = new GetAllMenuGroupQueryRequest(
            new PaginationFilter(1, 10),
            null,
            null
        );

        _uow.SectionGroups.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<object?>(), Arg.Any<object?>(), SectionType.Menu, Arg.Any<CancellationToken>())
            .Returns(new ListDto(0, Enumerable.Empty<SectionGroupDto>()));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Data.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_WhenErrorOccurs_ShouldThrowException()
    {
        // Arrange
        var request = new GetAllMenuGroupQueryRequest(
            new PaginationFilter(1, 10),
            null,
            null
        );

        _uow.SectionGroups.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<object?>(), Arg.Any<object?>(), SectionType.Menu, Arg.Any<CancellationToken>())
            .Throws(new System.Exception("Error occurred"));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<System.Exception>().WithMessage("Error occurred");
    }
}