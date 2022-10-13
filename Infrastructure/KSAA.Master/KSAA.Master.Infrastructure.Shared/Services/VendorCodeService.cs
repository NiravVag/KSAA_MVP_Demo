﻿using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Features.Master.Commands.VendorCodeCommand;
using KSAA.Master.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class VendorCodeService : IVendorCodeService
    {
        private readonly IGenericRepositoryAsync<VendorCode> _VendorCodeRepositoryAsync;
        private readonly IMapper _mapper;

        public VendorCodeService(IGenericRepositoryAsync<VendorCode> VendorCodeRepositoryAsync, IMapper mapper)
        {
            _VendorCodeRepositoryAsync = VendorCodeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<VendorCodeViewModel> AddVendorCode(CreateVendorCodeCommand command)
        {
            var applicationVendorCode = _mapper.Map<VendorCode>(command);
            applicationVendorCode.CreatedOn = DateTime.Now;
            applicationVendorCode.IsActive = Domain.Entities.IsActive.Active;
            await _VendorCodeRepositoryAsync.AddAsync(applicationVendorCode);

            return _mapper.Map<VendorCodeViewModel>(applicationVendorCode);
        }

        public async Task DeleteVendorCode(DeleteVendorCodeCommand command)
        {
            if (command.Id > 0)
            {
                var applicationVendorCode = await _VendorCodeRepositoryAsync.FindById(command.Id);
                applicationVendorCode.IsActive = Domain.Entities.IsActive.Delete;
                await _VendorCodeRepositoryAsync.UpdateAsync(applicationVendorCode);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<VendorCodeViewModel> EditVendorCode(UpdateVendorCodeCommand command)
        {
            var applicationVendorCode = _mapper.Map<UpdateVendorCodeCommand>(command);
            applicationVendorCode.IsActive = Domain.Entities.IsActive.Active;
            applicationVendorCode.ModifiedOn = DateTime.Now;
            var applicationUser = await _VendorCodeRepositoryAsync.FindById(applicationVendorCode.Id);
            _mapper.Map(command, applicationUser);

            await _VendorCodeRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<VendorCodeViewModel>(applicationUser);
        }

        public async Task<VendorCodeViewModel> GetVendorCodeById(long id)
        {
            var applicationVendorCode = await _VendorCodeRepositoryAsync.FindById(id);
            return _mapper.Map<VendorCodeViewModel>(applicationVendorCode);
        }

        public async Task<List<VendorCodeViewModel>> GetVendorCodeList()
        {
            var VendorCodeList = await _VendorCodeRepositoryAsync.GetAllAsync();
            return _mapper.Map<List<VendorCodeViewModel>>(VendorCodeList);

        }
    }
}