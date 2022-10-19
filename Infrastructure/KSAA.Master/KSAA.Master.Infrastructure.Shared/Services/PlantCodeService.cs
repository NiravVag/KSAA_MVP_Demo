﻿using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Entities.Master;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master.PlantCodeDTOs;
using KSAA.Master.Application.Features.Master.Commands.PlantCodeCommand;
using KSAA.Master.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class PlantCodeService : IPlantCodeService
    {
        private readonly IGenericRepositoryAsync<PlantCode> _PlantCodeRepositoryAsync;
        private readonly IMapper _mapper;

        public PlantCodeService(IGenericRepositoryAsync<PlantCode> PlantCodeRepositoryAsync, IMapper mapper)
        {
            _PlantCodeRepositoryAsync = PlantCodeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PlantCodeViewModel> AddPlantCode(CreatePlantCodeCommand command)
        {
            var applicationPlantCode = _mapper.Map<PlantCode>(command);
            applicationPlantCode.IsActive = IsActive.Active;
            applicationPlantCode.CreatedBy = 0;
            applicationPlantCode.CreatedOn = DateTime.Now;
            applicationPlantCode.ModifiedBy = 0;
            applicationPlantCode.ModifiedOn = DateTime.Now;
            await _PlantCodeRepositoryAsync.AddAsync(applicationPlantCode);

            return _mapper.Map<PlantCodeViewModel>(applicationPlantCode);
        }

        public async Task DeletePlantCode(DeletePlantCodeCommand command)
        {
            if (command.Id > 0)
            {
                var applicationPlantCode = await _PlantCodeRepositoryAsync.FindById(command.Id);
                applicationPlantCode.IsActive = Domain.Entities.IsActive.Delete;
                await _PlantCodeRepositoryAsync.UpdateAsync(applicationPlantCode);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<PlantCodeViewModel> EditPlantCode(UpdatePlantCodeCommand command)
        {
            var applicationPlantCode = _mapper.Map<UpdatePlantCodeCommand>(command);
            applicationPlantCode.IsActive = Domain.Entities.IsActive.Active;
            applicationPlantCode.ModifiedOn = DateTime.Now;
            var applicationUser = await _PlantCodeRepositoryAsync.FindById(applicationPlantCode.Id);
            _mapper.Map(command, applicationUser);

            await _PlantCodeRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<PlantCodeViewModel>(applicationUser);
        }

        public async Task<PlantCodeViewModel> GetPlantCodeById(long id)
        {
            var applicationPlantCode = await _PlantCodeRepositoryAsync.FindById(id);
            return _mapper.Map<PlantCodeViewModel>(applicationPlantCode);
        }

        public async Task<IEnumerable<PlantCodeViewModel>> GetPlantCodeList()
        {
            var PlantCodeList = await _PlantCodeRepositoryAsync.GetAllAsync();
            PlantCodeList.OrderByDescending(x => x.Id).ToList();
            return _mapper.Map<List<PlantCodeViewModel>>(PlantCodeList).Where(x => x.IsActive != IsActive.Delete);

        }
    }
}