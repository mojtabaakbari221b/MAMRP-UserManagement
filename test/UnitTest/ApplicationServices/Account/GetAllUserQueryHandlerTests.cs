namespace UnitTest.ApplicationServices.Account;

public class GetAllUserQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetAllUserQueryHandler _handler;

    public GetAllUserQueryHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new GetAllUserQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectPaginationResult()
    {
        // Arrange
        var pagination = new PaginationFilter { PageNumber = 1, PageSize = 10 };
        var filtering = new UserFiltering();
        var ordering = new UserOrdering();
        var request = new GetAllUserQueryRequest(pagination, filtering, ordering);

        var mockUsers = new List<GetUserQueryResponse>
        {
            new GetUserQueryResponse(Guid.NewGuid(), "user1", "John", "Doe"),
            new GetUserQueryResponse(Guid.NewGuid(), "user2", "Jane", "Smith")
        };

        var listDto = new ListDto
        (
            2,
            mockUsers.Select(u => u.Adapt<GetUserQueryResponse>()).ToList()
        );

        _uow.Users.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<UserFiltering>(), Arg.Any<UserOrdering>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(listDto));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.PageNumber.Should().Be(pagination.PageNumber);
        result.PageSize.Should().Be(pagination.PageSize);
        result.TotalRecords.Should().Be(listDto.Count);
        result.Data.Should().BeEquivalentTo(mockUsers);
    }

    [Fact]
    public async Task Handle_ShouldCallGetAllMethodOnce()
    {
        // Arrange
        var pagination = new PaginationFilter { PageNumber = 1, PageSize = 10 };
        var filtering = new UserFiltering();
        var ordering = new UserOrdering();
        var request = new GetAllUserQueryRequest(pagination, filtering, ordering);

        var mockUsers = new List<GetUserQueryResponse>
        {
            new(Guid.NewGuid(), "user1", "John", "Doe"),
            new(Guid.NewGuid(), "user2", "Jane", "Smith")
        };

        var listDto = new ListDto
        (
            2,
            mockUsers.Select(u => u.Adapt<GetUserQueryResponse>()).ToList()
        );

        _uow.Users.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<UserFiltering>(), Arg.Any<UserOrdering>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(listDto));

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        await _uow.Users.Received(1).GetAll(pagination, filtering, ordering, Arg.Any<CancellationToken>()); // بررسی فراخوانی متد
    }
}