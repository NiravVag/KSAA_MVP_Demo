using KSAA.Master.Application.Features.Master.Commands;
using KSAA.Master.Application.Features.Master.Queries;
using KSAA.Master.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSAA.Master.ApiServices.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DocumentTypeController : BaseApiController
    {
        private readonly IDocumentTypeService documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            this.documentTypeService = documentTypeService;
        }


        [HttpPost]
        [Route("CreateDocumentType")]
        public async Task<IActionResult> Post(CreateDocumentTypeCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpPost]
        [Route("GetAllDocumentType")]
        public async Task<IActionResult> GetAllDocumentType(GetAllDocumentTypeQuery command)
        {

            return this.Ok(await this.Mediator.Send(command));

        }

        [HttpGet]
        [Route("GetDocumentTypeById/{id}")]
        public async Task<IActionResult> GetDocumentTypeById(int id)
        {
            var getDocumentTypeByIdQuery = new GetDocumentTypeByIdQuery()
            {
                Id = id,
            };
            return this.Ok(await this.Mediator.Send(getDocumentTypeByIdQuery));
        }

        [HttpPut]
        [Route("UpdateDocumentTypeById")]
        public async Task<IActionResult> UpdateDocumentTypeById(UpdateDocumentTypeCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpDelete("DeleteDocumentTypeById/{id}")]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            var deleteUserByIdQuery = new DeleteDocumentTypeCommand()
            {
                Id = id,
            };
            return this.Ok(await this.Mediator.Send(deleteUserByIdQuery));
        }
    }
}