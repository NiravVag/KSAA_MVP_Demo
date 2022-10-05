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

namespace KSAA.Master.Application.Features.Master.Queries.DocumetTypeQueries
{
    public class GetAllDocumentTypeQuery : IRequest<Response<IEnumerable<DocumentTypeViewModel>>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllDocumentTypeQuery, Response<IEnumerable<DocumentTypeViewModel>>>
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IDocumentTypeService documentTypeService, IMapper mapper)
        {
            _documentTypeService = documentTypeService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<DocumentTypeViewModel>>> Handle(GetAllDocumentTypeQuery request, CancellationToken cancellationToken)
        {
            var documentTypeList = await _documentTypeService.GetDocumentTypeList();

            return new Response<IEnumerable<DocumentTypeViewModel>>(documentTypeList);
        }
    }
}