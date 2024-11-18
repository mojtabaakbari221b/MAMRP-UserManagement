namespace UnitTest.ApplicationServices.Account;

public class LoginCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenFactory _tokenFactory;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        _tokenFactory = Substitute.For<ITokenFactory>();
        
        _handler = new LoginCommandHandler(_uow, _tokenFactory);
    }

    [Fact]
    public async Task Handle_WhenLoginFails_ShouldThrowLoginFailedException()
    {
        // Arrange
        var request = new LoginCommandRequest("testuser", "wrongpassword", "wrongtoken");

        _uow.Users.Login(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromResult(OperationResult<LoginResult>.Failure("Invalid credentials", ErrorType.Errors)));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<LoginFailedException>();
    }

    [Fact]
    public async Task Handle_WhenLoginSucceeds_ShouldCreateAndSaveToken()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new LoginCommandRequest("testuser", "correctpassword", "wrongpassword");
        var loginResult = OperationResult<LoginResult>.Success(new LoginResult(true, userId));
        var token = new TokenResult("access_token", "refresh_token", Guid.NewGuid());

        _uow.Users.Login(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromResult(loginResult));

        _tokenFactory.CreateTokenAsync(userId)
            .Returns(Task.FromResult(token));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should()
            .BeEquivalentTo(token.Adapt<LoginCommandResponse>());
        
        await _uow.Users.Received(1).SaveToken(Arg.Any<TokenDto>());
    }
}