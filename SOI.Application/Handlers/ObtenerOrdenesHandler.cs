    using AutoMapper;
    using MediatR;
    using SOI.Application.DTOs;
    using SOI.Application.Interfaces.Repositories;
    using SOI.Application.Queries;

    namespace SOI.Application.Handlers;

    public class ObtenerOrdenesHandler : IRequestHandler<ObtenerOrdenesQuery, List<OrdenResponseDto>>
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IMapper _mapper;
        
        public ObtenerOrdenesHandler(IOrdenRepository ordenRepository, IMapper mapper)
        {
            _ordenRepository = ordenRepository;
            _mapper = mapper;
        }
        
        public async Task<List<OrdenResponseDto>> Handle(ObtenerOrdenesQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await _ordenRepository.GetAllAsync();
            return _mapper.Map<List<OrdenResponseDto>>(ordenes);
        }
    }