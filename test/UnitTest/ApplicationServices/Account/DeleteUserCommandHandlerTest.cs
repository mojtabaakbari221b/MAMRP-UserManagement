namespace UnitTest.ApplicationServices.Account;


public class DeleteUserCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new DeleteUserCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowUserNotFoundException()
    {
        // Arrange
        var request = new DeleteUserCommandRequest(Guid.NewGuid());

        _uow.Users.AnyAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(OperationResult<bool>.Success(false)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UserNotFoundException>(); // بررسی پرتاب استثنا
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldCallDeleteMethod()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new DeleteUserCommandRequest(userId);

        _uow.Users.AnyAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(OperationResult<bool>.Success(true)));

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        await _uow.Users.Received(1).Delete(userId, Arg.Any<CancellationToken>());
    }
}