using MediatR;


public class AddMenuCommand() : IRequest<int> {
    public long GroupId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
}