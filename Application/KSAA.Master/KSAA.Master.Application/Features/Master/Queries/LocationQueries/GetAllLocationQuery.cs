﻿using AutoMapper;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Interfaces.Services;
using KSAA.Master.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.Features.Master.Queries.LocationQueries
{
    public class GetAllLocationQuery : IRequest<Response<IEnumerable<LocationViewModel>>>
    {
    }

    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, Response<IEnumerable<LocationViewModel>>>
    {
        private readonly ILocationService _LocationService;
        private readonly IMapper _mapper;

        public GetAllLocationQueryHandler(ILocationService LocationService, IMapper mapper)
        {
            _LocationService = LocationService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<LocationViewModel>>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var LocationList = await _LocationService.GetLocationList();

            return new Response<IEnumerable<LocationViewModel>>(LocationList);
        }
    }
}
