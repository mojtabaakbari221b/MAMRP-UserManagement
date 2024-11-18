namespace UnitTest.ApplicationServices.MenuGroups;

public class DeleteMenuGroupCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly DeleteMenuGroupCommandHandler _handler;

    public DeleteMenuGroupCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new DeleteMenuGroupCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenSectionGroupExists_ShouldDeleteSectionGroup()
    {
        // Arrange
        var request = new DeleteMenuGroupCommandRequest(1);

        var sectionGroup = new SectionGroup { Id = 1, Name = "Menu Group", Type = SectionType.Menu };
        _uow.SectionGroups.FindAsync(Arg.Any<long>(), Arg.Any<SectionType>(), Arg.Any<CancellationToken>())
            .Returns(sectionGroup);

        _uow.SectionGroups.Delete(Arg.Any<SectionGroup>());
        _uow.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _uow.SectionGroups.Received(1).Delete(Arg.Any<SectionGroup>());
        await _uow.SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenSectionGroupNotFound_ShouldThrowSectionGroupNotFoundException()
    {
        // Arrange
        var request = new DeleteMenuGroupCommandRequest(1);

        _uow.SectionGroups.FindAsync(Arg.Any<long>(), Arg.Any<SectionType>(), Arg.Any<CancellationToken>())
            .Returns((SectionGroup?)null);

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<MenuGroupNotFoundException>();
    }
}