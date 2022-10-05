using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Features.Master.Commands;
using KSAA.Master.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            applicationDocumentType.CreatedOn = DateTime.Now;
            applicationDocumentType.IsActive = Domain.Entities.IsActive.Active;
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
            applicationDocumentType.ModifiedOn = DateTime.Now;
            applicationDocumentType.IsActive = Domain.Entities.IsActive.Active;
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

        public async Task<List<DocumentTypeViewModel>> GetDocumentTypeList()
        {
            var documentTypeList = await _documentTypeRepositoryAsync.GetAllAsync();
            return _mapper.Map<List<DocumentTypeViewModel>>(documentTypeList);

        }
    }
}