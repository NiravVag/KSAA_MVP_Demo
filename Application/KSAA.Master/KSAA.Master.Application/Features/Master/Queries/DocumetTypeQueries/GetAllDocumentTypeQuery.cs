using AutoMapper;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
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

    public class GetAllDocumentTypeQueryHandler : IRequestHandler<GetAllDocumentTypeQuery, Response<IEnumerable<DocumentTypeViewModel>>>
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IMapper _mapper;

        public GetAllDocumentTypeQueryHandler(IDocumentTypeService documentTypeService, IMapper mapper)
        {
            _documentTypeService = documentTypeService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<DocumentTypeViewModel>>> Handle(GetAllDocumentTypeQuery request, CancellationToken cancellationToken)
        {
            //return await _documentTypeService.GetDocumentTypeList();
            var docList = await _documentTypeService.GetDocumentTypeList();
            return new Response<IEnumerable<DocumentTypeViewModel>>(docList);

        }
    }
}