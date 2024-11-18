using UserManagement.Application.ApplicationServices.Roles.Commands.Add;
using UserManagement.Application.ApplicationServices.Roles.Exceptions;

namespace UnitTest.ApplicationServices.Roles;

public class AddRoleCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly AddRoleCommandHandler _handler;

    public AddRoleCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new AddRoleCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenRoleAlreadyExists_ShouldThrowRoleAlreadyExistException()
    {
        // Arrange
        var request = new AddRoleCommandRequest("Admin", "Administrator");

        _uow.Roles.RoleExistsAsync(request.RoleName)
            .Returns(Task.FromResult(OperationResult.Success()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleAlredyExistException>();
    }

    [Fact]
    public async Task Handle_WhenRoleAdditionFails_ShouldThrowRoleNotAddedException()
    {
        // Arrange
        var request = new AddRoleCommandRequest("Admin", "Administrator");

        _uow.Roles.RoleExistsAsync(request.RoleName)
            .Returns(Task.FromResult(OperationResult.Failure(ErrorType.NotFound)));

        _uow.Roles.AddRole(request.RoleName, request.DisplayName)
            .Returns(Task.FromResult(OperationResult.Failure(["Failed to add role"], ErrorType.Errors)));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleNotAddedException>();
    }

    [Fact]
    public async Task Handle_WhenRoleAddedSuccessfully_ShouldReturnResponse()
    {
        // Arrange
        var request = new AddRoleCommandRequest("Admin", "Administrator");

        _uow.Roles.RoleExistsAsync(request.RoleName)
            .Returns(Task.FromResult(OperationResult.Failure(ErrorType.NotFound)));

        _uow.Roles.AddRole(request.RoleName, request.DisplayName)
            .Returns(Task.FromResult(OperationResult.Success()));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.RoleName.Should().Be(request.RoleName);
        result.DisplayName.Should().Be(request.DisplayName);
    }
}