namespace UnitTest.ApplicationServices.ServiceGroups;

public class UpdateServiceGroupCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly UpdateServiceGroupCommandHandler _handler;

    public UpdateServiceGroupCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new UpdateServiceGroupCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenSectionGroupExists_ShouldUpdateSectionGroup()
    {
        // Arrange
        var request = new UpdateServiceGroupCommandRequest(1, "Updated Name");

        var sectionGroup = new SectionGroup
        {
            Id = 1,
            Name = "Old Name",
            Type = SectionType.Service
        };


        _uow.SectionGroups.FindAsync(Arg.Any<long>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(sectionGroup);

        _uow.SectionGroups.Update(Arg.Any<SectionGroup>());
        _uow.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        sectionGroup.Name.Should().Be("Updated Name");
        _uow.SectionGroups.Received(1).Update(Arg.Any<SectionGroup>());
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenSectionGroupDoesNotExist_ShouldThrowServiceGroupNotFoundException()
    {
        // Arrange
        var request = new UpdateServiceGroupCommandRequest(1, "Updated Name");

        _uow.SectionGroups.FindAsync(Arg.Any<long>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(default(SectionGroup));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceGroupNotFoundException>();
    }
}