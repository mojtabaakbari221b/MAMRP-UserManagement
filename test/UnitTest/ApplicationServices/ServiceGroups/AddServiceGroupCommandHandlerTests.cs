namespace UnitTest.ApplicationServices.ServiceGroups;

public class AddServiceGroupCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly AddServiceGroupCommandHandler _handler;

    public AddServiceGroupCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new AddServiceGroupCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenValidRequest_ShouldAddNewSectionGroup()
    {
        // Arrange
        var request = new AddServiceGroupCommandRequest("New Service Group");

        _uow.SectionGroups.AddAsync(Arg.Any<SectionGroup>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _uow.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("New Service Group");
    }
}