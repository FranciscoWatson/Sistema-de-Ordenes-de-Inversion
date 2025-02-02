using MediatR;
using SOI.Application.Commands;
using SOI.Application.Interfaces.Repositories;

namespace SOI.Application.Handlers;

public class EliminarOrderHandler : IRequestHandler<EliminarOrderCommand>
{
    private readonly IOrdenRepository _ordenRepository;
    
    public EliminarOrderHandler(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public async Task Handle(EliminarOrderCommand request, CancellationToken cancellationToken)
    {
        await _ordenRepository.DeleteAsync(request.OrdenId);
    }
}