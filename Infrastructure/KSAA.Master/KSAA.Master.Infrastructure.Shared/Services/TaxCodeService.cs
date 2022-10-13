using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Features.Commands.TaxCodeCommand;
using KSAA.Master.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class TaxCodeService : ITaxCodeService
    {
        private readonly IGenericRepositoryAsync<TaxCode> _taxCodeRepositoryAsync;
        private readonly IMapper _mapper;

        public TaxCodeService(IGenericRepositoryAsync<TaxCode> taxCodeRepositoryAsync, IMapper mapper)
        {
            _taxCodeRepositoryAsync = taxCodeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<TaxCodeViewModel> AddTaxCode(CreateTaxCodeCommand command)
        {
            var applicationTaxCode = _mapper.Map<TaxCode>(command);
            applicationTaxCode.CreatedOn = DateTime.Now;
            applicationTaxCode.IsActive = Domain.Entities.IsActive.Active;
            await _taxCodeRepositoryAsync.AddAsync(applicationTaxCode);

            return _mapper.Map<TaxCodeViewModel>(applicationTaxCode);
        }

        public async Task DeleteTaxCode(DeleteTaxCodeCommand command)
        {
            if (command.Id > 0)
            {
                var applicationTaxCode = await _taxCodeRepositoryAsync.FindById(command.Id);
                applicationTaxCode.IsActive = Domain.Entities.IsActive.Delete;
                await _taxCodeRepositoryAsync.UpdateAsync(applicationTaxCode);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<TaxCodeViewModel> EditTaxCode(UpdateTaxCodeCommand command)
        {
            var applicationTaxCode = _mapper.Map<UpdateTaxCodeCommand>(command);
            applicationTaxCode.IsActive = Domain.Entities.IsActive.Active;
            applicationTaxCode.ModifiedOn = DateTime.Now;
            var applicationUser = await _taxCodeRepositoryAsync.FindById(applicationTaxCode.Id);
            _mapper.Map(command, applicationUser);

            await _taxCodeRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<TaxCodeViewModel>(applicationUser);
        }

        public async Task<TaxCodeViewModel> GetTaxCodeById(long id)
        {
            var applicationTaxCode = await _taxCodeRepositoryAsync.FindById(id);
            return _mapper.Map<TaxCodeViewModel>(applicationTaxCode);
        }

        public async Task<List<TaxCodeViewModel>> GetTaxCodeList()
        {
            var TaxCodeList = await _taxCodeRepositoryAsync.GetAllAsync();
            return _mapper.Map<List<TaxCodeViewModel>>(TaxCodeList);

        }
    }
}