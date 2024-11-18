namespace UnitTest.ApplicationServices.MenuGroups;

public class GetMenuGroupByIdQueryHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GetMenuGroupByIdQueryHandler _handler;

    public GetMenuGroupByIdQueryHandlerTests()
    {
        // شبیه‌سازی IUnitOfWork
        _uow = Substitute.For<IUnitOfWork>();

        // ایجاد هندلر
        _handler = new GetMenuGroupByIdQueryHandler(_uow);
    }

    [Fact]
    public async Task Handle_WhenMenuGroupExists_ShouldReturnMenuGroup()
    {
        // Arrange
        var request = new GetMenuGroupByIdQueryRequest(1);

        var sectionGroup = new SectionGroupDto(1, "Menu Group 1");

        // شبیه‌سازی GetById برای برگشت دادن گروه منو
        _uow.SectionGroups.GetById(1, SectionType.Menu, Arg.Any<CancellationToken>())
            .Returns(sectionGroup);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Menu Group 1");
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WhenMenuGroupNotFound_ShouldThrowException()
    {
        // Arrange
        var request = new GetMenuGroupByIdQueryRequest(1);

        // شبیه‌سازی GetById که هیچ داده‌ای برنگرداند (گروه منو پیدا نشد)
        _uow.SectionGroups.GetById(1, SectionType.Menu, Arg.Any<CancellationToken>())
            .Returns(default(SectionGroupDto));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceGroupNotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenErrorOccurs_ShouldThrowException()
    {
        // Arrange
        var request = new GetMenuGroupByIdQueryRequest(1);

        // شبیه‌سازی خطا در متد GetById
        _uow.SectionGroups.GetById(1, SectionType.Menu, Arg.Any<CancellationToken>())
            .Throws(new System.Exception("Error occurred"));

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<System.Exception>().WithMessage("Error occurred");
    }
}