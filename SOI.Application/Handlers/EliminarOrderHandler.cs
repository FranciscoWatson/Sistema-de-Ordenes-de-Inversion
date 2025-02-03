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
        var order = await _ordenRepository.GetByIdAsync(request.OrdenId);
        if (order == null || order.CuentaId != request.CuentaId)
            throw new UnauthorizedAccessException("No tienes permiso para eliminar esta orden.");
        
        await _ordenRepository.DeleteAsync(request.OrdenId);
    }
}