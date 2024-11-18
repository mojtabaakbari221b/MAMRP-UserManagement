namespace UnitTest.ApplicationServices.Account;

public class LoginByRefreshTokenCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenFactory _tokenFactory;
    private readonly LoginByRefreshTokenCommandHandler _handler;

    public LoginByRefreshTokenCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        _tokenFactory = Substitute.For<ITokenFactory>();
        
        _handler = new LoginByRefreshTokenCommandHandler(_tokenFactory, _uow);
    }

    [Fact]
    public async Task Handle_WhenTokensAreCreated_ShouldSaveTokenAndReturnLoginCommandResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new LoginByRefreshTokenCommandRequest(userId);
        var token = new TokenResult("access_token", "refresh_token", Guid.NewGuid());

        _tokenFactory.CreateTokenAsync(userId)
            .Returns(Task.FromResult(token));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new LoginCommandResponse("access_token", "refresh_token")); // بررسی که LoginCommandResponse بازگشت داده شده

        await _uow.Users.Received(1).SaveToken(Arg.Any<TokenDto>());
    }
}