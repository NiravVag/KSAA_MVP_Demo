using AutoMapper;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Interfaces.Services;
using KSAA.Master.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.PlantCodeE.Application.Features.PlantCode.Queries
{
    public class GetPlantCodeByIdQuery : IRequest<Response<PlantCodeViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetPlantCodeByIdQuery, Response<PlantCodeViewModel>>
    {
        private readonly IPlantCodeService _PlantCodeService;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IPlantCodeService PlantCodeService, IMapper mapper)
        {
            _PlantCodeService = PlantCodeService;
            _mapper = mapper;
        }
        public async Task<Response<PlantCodeViewModel>> Handle(GetPlantCodeByIdQuery request, CancellationToken cancellationToken)
        {
            var gePlantCodeById = await _PlantCodeService.GetPlantCodeById(request.Id);
            return new Response<PlantCodeViewModel>(gePlantCodeById);
        }
    }
}
