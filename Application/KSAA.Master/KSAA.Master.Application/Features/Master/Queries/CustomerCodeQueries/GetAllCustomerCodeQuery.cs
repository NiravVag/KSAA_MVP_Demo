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

namespace KSAA.Master.Application.Features.Master.Queries.CustomerCodeQueries
{
    public class GetAllCustomerCodeQuery : IRequest<Response<IEnumerable<CustomerCodeViewModel>>>
    {
    }

    public class GetAllCustomerCodeQueryHandler : IRequestHandler<GetAllCustomerCodeQuery, Response<IEnumerable<CustomerCodeViewModel>>>
    {
        private readonly ICustomerCodeService _CustomerCodeService;
        private readonly IMapper _mapper;

        public GetAllCustomerCodeQueryHandler(ICustomerCodeService CustomerCodeService, IMapper mapper)
        {
            _CustomerCodeService = CustomerCodeService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<CustomerCodeViewModel>>> Handle(GetAllCustomerCodeQuery request, CancellationToken cancellationToken)
        {
            var CustomerCodeList = await _CustomerCodeService.GetCustomerCodeList();

            return new Response<IEnumerable<CustomerCodeViewModel>>(CustomerCodeList);
        }
    }
}