using UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Delete;

namespace UnitTest.ApplicationServices.ServiceGroups;

public class DeleteServiceGroupCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly DeleteServiceGroupCommandHandler _handler;

    public DeleteServiceGroupCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new DeleteServiceGroupCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenSectionGroupExists_ShouldDeleteSectionGroup()
    {
        // Arrange
        var request = new DeleteServiceGroupCommandRequest(1);

        var sectionGroup = new SectionGroup
        {
            Id = 1,
            Name = "Test Service Group",
            Type = SectionType.Service
        };

        _uow.SectionGroups.FindAsync(Arg.Any<long>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(sectionGroup);

        _uow.SectionGroups.Delete(Arg.Any<SectionGroup>());
        _uow.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _uow.SectionGroups.Received(1).Delete(Arg.Any<SectionGroup>());
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenSectionGroupDoesNotExist_ShouldThrowServiceGroupNotFoundException()
    {
        // Arrange
        var request = new DeleteServiceGroupCommandRequest(1);

        _uow.SectionGroups.FindAsync(Arg.Any<long>(), SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(default(SectionGroup));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceGroupNotFoundException>();
    }
    
}