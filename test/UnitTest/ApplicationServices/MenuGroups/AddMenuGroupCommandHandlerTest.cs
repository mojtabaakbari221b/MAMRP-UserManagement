namespace UnitTest.ApplicationServices.MenuGroups;

public class AddMenuGroupCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly AddMenuGroupCommandHandler _handler;

    public AddMenuGroupCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new AddMenuGroupCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenRequestIsValid_ShouldAddSectionGroup()
    {
        // Arrange
        var request = new AddMenuGrcoupCommandRequest("New Menu Group");

        _uow.SectionGroups.AddAsync(Arg.Any<SectionGroup>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _uow.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("New Menu Group");

        await _uow.SectionGroups.Received(1).AddAsync(Arg.Any<SectionGroup>(), Arg.Any<CancellationToken>());
        await _uow.SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}