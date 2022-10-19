using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Entities.Master;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.Features.Master.Commands.DocumentTypeCommand;
using KSAA.Master.Application.Interfaces.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IGenericRepositoryAsync<DocumentType> _documentTypeRepositoryAsync;
        private readonly IMapper _mapper;

        public DocumentTypeService(IGenericRepositoryAsync<DocumentType> documentTypeRepositoryAsync, IMapper mapper)
        {
            _documentTypeRepositoryAsync = documentTypeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<DocumentTypeViewModel> AddDocumentType(CreateDocumentTypeCommand command)
        {

            var applicationDocumentType = _mapper.Map<DocumentType>(command);
            applicationDocumentType.IsActive = IsActive.Active;
            applicationDocumentType.CreatedBy = 0;
            applicationDocumentType.CreatedOn = DateTime.Now;
            applicationDocumentType.ModifiedBy = 0;
            applicationDocumentType.ModifiedOn = DateTime.Now;
            await _documentTypeRepositoryAsync.AddAsync(applicationDocumentType);

            return _mapper.Map<DocumentTypeViewModel>(applicationDocumentType);
        }

        public async Task DeleteDocumentType(DeleteDocumentTypeCommand command)
        {
            if (command.Id > 0)
            {
                var applicationDocumentType = await _documentTypeRepositoryAsync.FindById(command.Id);
                applicationDocumentType.IsActive = Domain.Entities.IsActive.Delete;
                await _documentTypeRepositoryAsync.UpdateAsync(applicationDocumentType);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<DocumentTypeViewModel> EditDocumentType(UpdateDocumentTypeCommand command)
        {
            var applicationDocumentType = _mapper.Map<UpdateDocumentTypeCommand>(command);
            applicationDocumentType.IsActive = Domain.Entities.IsActive.Active;
            applicationDocumentType.ModifiedOn = DateTime.Now;
            var applicationUser = await _documentTypeRepositoryAsync.FindById(applicationDocumentType.Id);
            _mapper.Map(command, applicationUser);

            await _documentTypeRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<DocumentTypeViewModel>(applicationUser);
        }

        public async Task<DocumentTypeViewModel> GetDocumentTypeById(long id)
        {
            var applicationDocumentType = await _documentTypeRepositoryAsync.FindById(id);
            return _mapper.Map<DocumentTypeViewModel>(applicationDocumentType);
        }

        public async Task<IEnumerable<DocumentTypeViewModel>> GetDocumentTypeList()
        {
            var documentTypeList = await _documentTypeRepositoryAsync.GetAllAsync();
            documentTypeList.OrderByDescending(x => x.Id).AsEnumerable();
            return _mapper.Map<List<DocumentTypeViewModel>>(documentTypeList).Where(x => x.IsActive != IsActive.Delete);

        }
    }
}