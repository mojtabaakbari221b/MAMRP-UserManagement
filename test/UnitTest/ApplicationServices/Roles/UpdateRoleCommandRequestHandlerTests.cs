namespace UnitTest.ApplicationServices.Roles;

public class UpdateRoleCommandRequestHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly UpdateRoleCommandRequestHandler _handler;

    public UpdateRoleCommandRequestHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new UpdateRoleCommandRequestHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenRoleDoesNotExist_ShouldThrowRoleNotFoundException()
    {
        // Arrange
        var request = new UpdateRoleCommandRequest(Guid.NewGuid(), "NewRoleName", "NewDisplayName");

        _uow.Roles.RoleExistsAsync(request.Id)
            .Returns(Task.FromResult(OperationResult.Failure(ErrorType.NotFound)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleNotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenUpdateFails_ShouldThrowRoleNotUpdatedException()
    {
        // Arrange
        var roleId = Guid.NewGuid();
        var request = new UpdateRoleCommandRequest(roleId, "UpdatedRoleName", "UpdatedDisplayName");

        _uow.Roles.RoleExistsAsync(roleId)
            .Returns(Task.FromResult(OperationResult.Success()));

        _uow.Roles.Update(Arg.Any<RoleDto>())
            .Returns(Task.FromResult(OperationResult.Failure(["Failed to update role" ], ErrorType.Errors)));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleNotUpdatedException>();
    }

    [Fact]
    public async Task Handle_WhenRoleUpdatedSuccessfully_ShouldCompleteWithoutException()
    {
        // Arrange
        var roleId = Guid.NewGuid();
        var request = new UpdateRoleCommandRequest(roleId, "UpdatedRoleName", "UpdatedDisplayName");

        _uow.Roles.RoleExistsAsync(roleId)
            .Returns(Task.FromResult(OperationResult.Success()));

        _uow.Roles.Update(Arg.Any<RoleDto>())
            .Returns(Task.FromResult(OperationResult.Success()));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }
}