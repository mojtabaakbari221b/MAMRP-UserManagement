namespace UnitTest.ApplicationServices.Roles;

public class DeleteRoleCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly DeleteRoleCommandHandler _handler;

    public DeleteRoleCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new DeleteRoleCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenRoleDoesNotExist_ShouldThrowRoleNotFoundException()
    {
        // Arrange
        var request = new DeleteRoleCommandRequest(Guid.NewGuid());

        _uow.Roles.RoleExistsAsync(request.Id)
            .Returns(Task.FromResult(OperationResult.Failure(ErrorType.NotFound)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleNotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenDeleteFails_ShouldThrowRoleNotDeletedException()
    {
        // Arrange
        var roleId = Guid.NewGuid();
        var request = new DeleteRoleCommandRequest(roleId);

        _uow.Roles.RoleExistsAsync(roleId)
            .Returns(Task.FromResult(OperationResult.Success()));

        _uow.Roles.Delete(roleId)
            .Returns(Task.FromResult(OperationResult.Failure(ErrorType.NotFound)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleNotDeletedException>();
    }

    [Fact]
    public async Task Handle_WhenRoleDeletedSuccessfully_ShouldCompleteWithoutException()
    {
        // Arrange
        var roleId = Guid.NewGuid();
        var request = new DeleteRoleCommandRequest(roleId);

        _uow.Roles.RoleExistsAsync(roleId)
            .Returns(Task.FromResult(OperationResult.Success()));

        _uow.Roles.Delete(roleId)
            .Returns(Task.FromResult(OperationResult.Success()));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }
}