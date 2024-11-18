namespace UnitTest.ApplicationServices.MenuGroups;

public class UpdateMenuGroupCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly UpdateMenuGroupCommandHandler _handler;

    public UpdateMenuGroupCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new UpdateMenuGroupCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenSectionGroupExists_ShouldUpdateSectionGroup()
    {
        // Arrange
        var request = new UpdateMenuGroupCommandRequest(1, "Updated Menu Group");

        var sectionGroup = new SectionGroup { Id = 1, Name = "Old Menu Group", Type = SectionType.Menu };
        _uow.SectionGroups.FindAsync(Arg.Any<long>(), Arg.Any<SectionType>(), Arg.Any<CancellationToken>())
            .Returns(sectionGroup);

        _uow.SectionGroups.Update(Arg.Any<SectionGroup>());
        _uow.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        sectionGroup.Name.Should().Be("Updated Menu Group");

        _uow.SectionGroups.Received(1).Update(Arg.Any<SectionGroup>());
        await _uow.SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenSectionGroupNotFound_ShouldThrowServiceGroupNotFoundException()
    {
        // Arrange
        var request = new UpdateMenuGroupCommandRequest(1, "Updated Menu Group");

        _uow.SectionGroups.FindAsync(Arg.Any<long>(), Arg.Any<SectionType>(), Arg.Any<CancellationToken>())
            .Returns((SectionGroup?)null);

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceGroupNotFoundException>();
    }
}