using MediatR;


public class DeleteMenuCommand : IRequest
{
    public long Id { get; set; }
}

