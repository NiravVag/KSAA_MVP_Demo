﻿using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Entities.Master;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master.CompanyDTOs;
using KSAA.Master.Application.Features.Master.Commands.CompanyCommand;
using KSAA.Master.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IGenericRepositoryAsync<Company> _CompanyRepositoryAsync;
        private readonly IMapper _mapper;

        public CompanyService(IGenericRepositoryAsync<Company> CompanyRepositoryAsync, IMapper mapper)
        {
            _CompanyRepositoryAsync = CompanyRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<CompanyViewModel> AddCompany(CreateCompanyCommand command)
        {
            var applicationCompany = _mapper.Map<Company>(command);
            applicationCompany.IsActive = IsActive.Active;
            applicationCompany.CreatedBy = 0;
            applicationCompany.CreatedOn = DateTime.Now;
            applicationCompany.ModifiedBy = 0;
            applicationCompany.ModifiedOn = DateTime.Now;
            await _CompanyRepositoryAsync.AddAsync(applicationCompany);

            return _mapper.Map<CompanyViewModel>(applicationCompany);
        }

        public async Task DeleteCompany(DeleteCompanyCommand command)
        {
            if (command.Id > 0)
            {
                var applicationCompany = await _CompanyRepositoryAsync.FindById(command.Id);
                applicationCompany.IsActive = Domain.Entities.IsActive.Delete;
                await _CompanyRepositoryAsync.UpdateAsync(applicationCompany);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<CompanyViewModel> EditCompany(UpdateCompanyCommand command)
        {
            var applicationCompany = _mapper.Map<UpdateCompanyCommand>(command);
            applicationCompany.IsActive = Domain.Entities.IsActive.Active;
            applicationCompany.ModifiedOn = DateTime.Now;
            var applicationUser = await _CompanyRepositoryAsync.FindById(applicationCompany.Id);
            _mapper.Map(command, applicationUser);

            await _CompanyRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<CompanyViewModel>(applicationUser);
        }

        public async Task<CompanyViewModel> GetCompanyById(long id)
        {
            var applicationCompany = await _CompanyRepositoryAsync.FindById(id);
            return _mapper.Map<CompanyViewModel>(applicationCompany);
        }

        public async Task<IEnumerable<CompanyViewModel>> GetCompanyList()
        {
            var CompanyList = await _CompanyRepositoryAsync.GetAllAsync();
            CompanyList.OrderByDescending(x => x.Id).ToList();
            return _mapper.Map<List<CompanyViewModel>>(CompanyList).Where(x => x.IsActive != IsActive.Delete);

        }
    }
}
