﻿using AutoMapper;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Features.Master.Queries.DocumetTypeQueries;
using KSAA.Master.Application.Interfaces.Services;
using KSAA.Master.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.Features.Master.Queries.TBTaggingQueries
{
    public class GetAllTBTaggingQuery : IRequest<Response<IEnumerable<TBTaggingViewModel>>>
    {
    }

    public class GetAllTBTaggingQueryHandler : IRequestHandler<GetAllTBTaggingQuery, Response<IEnumerable<TBTaggingViewModel>>>
    {
        private readonly ITBTaggingService _TBTaggingService;
        private readonly IMapper _mapper;

        public GetAllTBTaggingQueryHandler(ITBTaggingService TBTaggingService, IMapper mapper)
        {
            _TBTaggingService = TBTaggingService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<TBTaggingViewModel>>> Handle(GetAllTBTaggingQuery request, CancellationToken cancellationToken)
        {
            var TBTaggingList = await _TBTaggingService.GetTBTaggingList();

            return new Response<IEnumerable<TBTaggingViewModel>>(TBTaggingList);
        }
    }
}