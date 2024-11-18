namespace UnitTest.ApplicationServices.Account;

public class UpdateUserCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        // شبیه‌سازی IUnitOfWork
        _uow = Substitute.For<IUnitOfWork>();

        // ایجاد هندلر
        _handler = new UpdateUserCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowUserNotFoundException()
    {
        // Arrange
        var request = new UpdateUserCommandRequest(Guid.NewGuid(), "user123", "John", "Doe");

        _uow.Users.AnyAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(OperationResult<bool>.Success(false)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UserNotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldCallUpdateMethod()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new UpdateUserCommandRequest(userId, "user123", "John", "Doe");

        _uow.Users.AnyAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(OperationResult<bool>.Success(true)));

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        await _uow.Users.Received(1).Update(Arg.Any<UserDto>(), Arg.Any<CancellationToken>());
    }
}