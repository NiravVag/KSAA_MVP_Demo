using KSAA.Master.Application.DTOs.Master.DocumentTypeDTOs;
using KSAA.Master.Application.Features.Master.Commands.DocumentTypeCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.Interfaces.Services
{
    public interface IDocumentTypeService
    {
        Task<DocumentTypeViewModel> AddDocumentType(CreateDocumentTypeCommand command);

        Task<DocumentTypeViewModel> EditDocumentType(UpdateDocumentTypeCommand command);

        Task<IEnumerable<DocumentTypeViewModel>> GetDocumentTypeList();

        Task<DocumentTypeViewModel> GetDocumentTypeById(long id);

        Task DeleteDocumentType(DeleteDocumentTypeCommand command);
    }
}
