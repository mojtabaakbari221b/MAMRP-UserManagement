using MediatR;

namespace 

public class UpdateMenuCommand : IRequest
{
    public long Id { get; set; }
    public long GroupId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
}

