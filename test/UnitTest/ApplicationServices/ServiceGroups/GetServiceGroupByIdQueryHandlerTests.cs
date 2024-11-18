namespace UnitTest.ApplicationServices.ServiceGroups;

public class GetServiceGroupByIdQueryHandlerTests
{
    private readonly IUnitOfWork _uowMock;
    private readonly GetServiceGroupByIdQueryHandler _handler;

    public GetServiceGroupByIdQueryHandlerTests()
    {
        _uowMock = Substitute.For<IUnitOfWork>();
        
        _handler = new GetServiceGroupByIdQueryHandler(_uowMock);
    }

    [Fact]
    public async Task Handle_WhenServiceGroupExists_ReturnsSectionGroupDto()
    {
        // Arrange
        var expectedSectionGroup = new SectionGroupDto(1, "Test Group");

        _uowMock.SectionGroups.GetById(1, SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(expectedSectionGroup);

        var request = new GetServiceGroupByIdQueryRequest(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Test Group");
    }

    [Fact]
    public async Task Handle_WhenServiceGroupDoesNotExist_ThrowsServiceGroupNotFoundException()
    {
        // Arrange
        _uowMock.SectionGroups.GetById(999, SectionType.Service, Arg.Any<CancellationToken>())
            .Returns(default(SectionGroupDto));

        var request = new GetServiceGroupByIdQueryRequest(999);

        // Act & Assert
        await _handler.Invoking(handler => handler.Handle(request, CancellationToken.None))
            .Should().ThrowAsync<ServiceGroupNotFoundException>();
    }
}