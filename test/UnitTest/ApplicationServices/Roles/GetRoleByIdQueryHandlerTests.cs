using UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

namespace UnitTest.ApplicationServices.Roles;


public class GetRoleByIdQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetRoleByIdQueryHandler _handler;

    public GetRoleByIdQueryHandlerTests()
    {
        // شبیه‌سازی IUnitOfWork
        _uow = Substitute.For<IUnitOfWork>();

        // ایجاد هندلر
        _handler = new GetRoleByIdQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenRoleDoesNotExist_ShouldThrowRoleNotFoundException()
    {
        // Arrange
        var request = new GetByIdRoleQueryReqeust(Guid.NewGuid());

        // شبیه‌سازی عدم وجود نقش
        _uow.Roles.GetRoleById(request.Id.ToString())
            .Returns(Task.FromResult(OperationResult<RoleDto?>.Success(null)));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleNotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenRoleExists_ShouldReturnRoleResponse()
    {
        // Arrange
        var roleId = Guid.NewGuid();
        var roleDto = new RoleDto(
            Id: roleId,
            Name: "Admin",
            DisplayName: "Administrator"
        );

        var request = new GetByIdRoleQueryReqeust(roleId);

        // شبیه‌سازی وجود نقش
        _uow.Roles.GetRoleById(request.Id.ToString())
            .Returns(Task.FromResult(OperationResult<RoleDto?>.Success(roleDto)));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(roleId);
        result.Name.Should().Be("Admin");
        result.DisplayName.Should().Be("Administrator");
    }
}