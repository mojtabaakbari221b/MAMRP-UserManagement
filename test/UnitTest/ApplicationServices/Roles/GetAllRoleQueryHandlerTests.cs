namespace UnitTest.ApplicationServices.Roles;


public class GetAllRoleQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetAllRoleQueryHandler _handler;

    public GetAllRoleQueryHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new GetAllRoleQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenNoRolesExist_ShouldReturnEmptyResult()
    {
        // Arrange
        var request = new GetAllRoleQueryRequest(
            new PaginationFilter(1, 10), 
            null, 
            null);

        _uow.Roles.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<RoleFiltering?>(), Arg.Any<RoleOrdering?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new ListDto(0, new List<GetRoleQueryResponse>())));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Data.Should().BeEmpty();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalRecords.Should().Be(0);
    }

    [Fact]
    public async Task Handle_WhenRolesExist_ShouldReturnPaginatedRoles()
    {
        // Arrange
        var roles = new List<GetRoleQueryResponse>
        {
            new(Guid.NewGuid(), "Admin", "Administrator"),
            new(Guid.NewGuid(), "User", "Regular User")
        };

        var request = new GetAllRoleQueryRequest(
            new PaginationFilter(1, 10), 
            null, 
            null);

        _uow.Roles.GetAll(Arg.Any<PaginationFilter>(), Arg.Any<RoleFiltering?>(), Arg.Any<RoleOrdering?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new ListDto(roles.Count, roles)));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Data.Should().HaveCount(2);
        result.Data.Should().ContainEquivalentOf(roles[0]);
        result.Data.Should().ContainEquivalentOf(roles[1]);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalRecords.Should().Be(2);
    }
}