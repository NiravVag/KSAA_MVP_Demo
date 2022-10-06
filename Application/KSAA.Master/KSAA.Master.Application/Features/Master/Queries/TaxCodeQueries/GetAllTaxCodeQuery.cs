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

namespace KSAA.Master.Application.Features.Master.Queries.TaxCodeQueries
{
    public class GetAllTaxCodeQuery : IRequest<Response<IEnumerable<TaxCodeViewModel>>>
    {
    }

    public class GetAllTaxCodeQueryHandler : IRequestHandler<GetAllTaxCodeQuery, Response<IEnumerable<TaxCodeViewModel>>>
    {
        private readonly ITaxCodeService _taxCodeService;
        private readonly IMapper _mapper;

        public GetAllTaxCodeQueryHandler(ITaxCodeService taxCodeService, IMapper mapper)
        {
            _taxCodeService = taxCodeService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<TaxCodeViewModel>>> Handle(GetAllTaxCodeQuery request, CancellationToken cancellationToken)
        {
            var taxCodeList = await _taxCodeService.GetTaxCodeList();

            return new Response<IEnumerable<TaxCodeViewModel>>(taxCodeList);
        }
    }
}