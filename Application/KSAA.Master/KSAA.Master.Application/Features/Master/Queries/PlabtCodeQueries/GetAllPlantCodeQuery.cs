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
    public class GetAllPlantCodeQuery : IRequest<Response<IEnumerable<PlantCodeViewModel>>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllPlantCodeQuery, Response<IEnumerable<PlantCodeViewModel>>>
    {
        private readonly IPlantCodeService _PlantCodeService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IPlantCodeService PlantCodeService, IMapper mapper)
        {
            _PlantCodeService = PlantCodeService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<PlantCodeViewModel>>> Handle(GetAllPlantCodeQuery request, CancellationToken cancellationToken)
        {
            var PlantCodeList = await _PlantCodeService.GetPlantCodeList();

            return new Response<IEnumerable<PlantCodeViewModel>>(PlantCodeList);
        }
    }
}