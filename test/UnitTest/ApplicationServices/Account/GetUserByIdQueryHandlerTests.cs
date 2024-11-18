namespace UnitTest.ApplicationServices.Account;

public class GetUserByIdQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new GetUserByIdQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserDto_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new GetUserByIdQueryRequest(userId);

        var mockUser = new UserDto(
            userId,
            "testuser",
            "John",
            "Doe",
            "",
            ""
        );

        var operationResult = OperationResult<UserDto?>.Success(mockUser);

        _uow.Users.GetUserById(userId.ToString())
            .Returns(Task.FromResult(operationResult));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(mockUser.UserId);
        result.UserName.Should().Be(mockUser.UserName);
        result.FirstName.Should().Be(mockUser.FirstName);
        result.FamilyName.Should().Be(mockUser.FamilyName);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new GetUserByIdQueryRequest(userId);

        var operationResult = OperationResult<UserDto?>.Success(null);

        _uow.Users.GetUserById(userId.ToString())
            .Returns(Task.FromResult(operationResult));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UserNotFoundException>();
    }
}