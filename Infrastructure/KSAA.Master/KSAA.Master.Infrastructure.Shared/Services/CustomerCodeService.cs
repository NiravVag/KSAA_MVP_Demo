using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Entities.Master;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master.CustomerCodeDTOs;
using KSAA.Master.Application.Features.Master.Commands.CustomerCodeCommand;
using KSAA.Master.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class CustomerCodeService : ICustomerCodeService
    {
        private readonly IGenericRepositoryAsync<CustomerCode> _CustomerCodeRepositoryAsync;
        private readonly IMapper _mapper;

        public CustomerCodeService(IGenericRepositoryAsync<CustomerCode> CustomerCodeRepositoryAsync, IMapper mapper)
        {
            _CustomerCodeRepositoryAsync = CustomerCodeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<CustomerCodeViewModel> AddCustomerCode(CreateCustomerCodeCommand command)
        {
            var applicationCustomerCode = _mapper.Map<CustomerCode>(command);
            applicationCustomerCode.IsActive = IsActive.Active;
            applicationCustomerCode.CreatedBy = 0;
            applicationCustomerCode.CreatedOn = DateTime.Now;
            applicationCustomerCode.ModifiedBy = 0;
            applicationCustomerCode.ModifiedOn = DateTime.Now;
            await _CustomerCodeRepositoryAsync.AddAsync(applicationCustomerCode);

            return _mapper.Map<CustomerCodeViewModel>(applicationCustomerCode);
        }

        public async Task DeleteCustomerCode(DeleteCustomerCodeCommand command)
        {
            if (command.Id > 0)
            {
                var applicationCustomerCode = await _CustomerCodeRepositoryAsync.FindById(command.Id);
                applicationCustomerCode.IsActive = Domain.Entities.IsActive.Delete;
                await _CustomerCodeRepositoryAsync.UpdateAsync(applicationCustomerCode);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<CustomerCodeViewModel> EditCustomerCode(UpdateCustomerCodeCommand command)
        {
            var applicationCustomerCode = _mapper.Map<UpdateCustomerCodeCommand>(command);
            applicationCustomerCode.IsActive = Domain.Entities.IsActive.Active;
            applicationCustomerCode.ModifiedOn = DateTime.Now;
            var applicationUser = await _CustomerCodeRepositoryAsync.FindById(applicationCustomerCode.Id);
            _mapper.Map(command, applicationUser);

            await _CustomerCodeRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<CustomerCodeViewModel>(applicationUser);
        }

        public async Task<CustomerCodeViewModel> GetCustomerCodeById(long id)
        {
            var applicationCustomerCode = await _CustomerCodeRepositoryAsync.FindById(id);
            return _mapper.Map<CustomerCodeViewModel>(applicationCustomerCode);
        }

        public async Task<IEnumerable<CustomerCodeViewModel>> GetCustomerCodeList()
        {
            var CustomerCodeList = await _CustomerCodeRepositoryAsync.GetAllAsync();
            CustomerCodeList.OrderByDescending(x => x.Id).ToList();
            return _mapper.Map<IEnumerable<CustomerCodeViewModel>>(CustomerCodeList).Where(x => x.IsActive != IsActive.Delete);

        }
    }
}