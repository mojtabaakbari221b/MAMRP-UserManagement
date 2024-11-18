namespace UnitTest.ApplicationServices.Account;

public class RegisterCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        
        _handler = new RegisterCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenRegistrationIsSuccessful_ShouldNotThrowException()
    {
        // Arrange
        var request = new RegisterCommandRequest("user123", "John", "Doe", "password123");

        var registerDto = new RegisterDto("user123", "John", "Doe", "password123");
        var registerResult = new RegisterResult(true);

        _uow.Users.Register(Arg.Any<RegisterDto>())
            .Returns(Task.FromResult(OperationResult<RegisterResult>.Success(registerResult)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Handle_WhenRegistrationFails_ShouldThrowUserNotRegisteredException()
    {
        // Arrange
        var request = new RegisterCommandRequest("user123", "John", "Doe", "password123");

        var registerDto = new RegisterDto("user123", "John", "Doe", "password123");
        var registerResult = new RegisterResult(false);

        _uow.Users.Register(Arg.Any<RegisterDto>())
            .Returns(Task.FromResult(OperationResult<RegisterResult>.Failure("Registration failed", ErrorType.Failure)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UserNotRegisteredException>();
    }
}