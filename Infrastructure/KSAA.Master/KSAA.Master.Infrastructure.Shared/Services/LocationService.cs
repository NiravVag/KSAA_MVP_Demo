using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Domain.Entities.Master;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.DTOs.Master.LocationDTOs;
using KSAA.Master.Application.Features.Master.Commands.LocationCommand;
using KSAA.Master.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Infrastructure.Shared.Services
{
    public class LocationService : ILocationService
    {
        private readonly IGenericRepositoryAsync<Location> _LocationRepositoryAsync;
        private readonly IMapper _mapper;

        public LocationService(IGenericRepositoryAsync<Location> LocationRepositoryAsync, IMapper mapper)
        {
            _LocationRepositoryAsync = LocationRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<LocationViewModel> AddLocation(CreateLocationCommand command)
        {
            var applicationLocation = _mapper.Map<Location>(command);
            applicationLocation.IsActive = IsActive.Active;
            applicationLocation.CreatedBy = 0;
            applicationLocation.CreatedOn = DateTime.Now;
            applicationLocation.ModifiedBy = 0;
            applicationLocation.ModifiedOn = DateTime.Now;
            await _LocationRepositoryAsync.AddAsync(applicationLocation);

            return _mapper.Map<LocationViewModel>(applicationLocation);
        }

        public async Task DeleteLocation(DeleteLocationCommand command)
        {
            if (command.Id > 0)
            {
                var applicationLocation = await _LocationRepositoryAsync.FindById(command.Id);
                applicationLocation.IsActive = Domain.Entities.IsActive.Delete;
                await _LocationRepositoryAsync.UpdateAsync(applicationLocation);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<LocationViewModel> EditLocation(UpdateLocationCommand command)
        {
            var applicationLocation = _mapper.Map<UpdateLocationCommand>(command);
            applicationLocation.IsActive = Domain.Entities.IsActive.Active;
            applicationLocation.ModifiedOn = DateTime.Now;
            var applicationUser = await _LocationRepositoryAsync.FindById(applicationLocation.Id);
            _mapper.Map(command, applicationUser);

            await _LocationRepositoryAsync.UpdateAsync(applicationUser);

            return _mapper.Map<LocationViewModel>(applicationUser);
        }

        public async Task<LocationViewModel> GetLocationById(long id)
        {
            var applicationLocation = await _LocationRepositoryAsync.FindById(id);
            return _mapper.Map<LocationViewModel>(applicationLocation);
        }

        public async Task<IEnumerable<LocationViewModel>> GetLocationList()
        {
            var LocationList = await _LocationRepositoryAsync.GetAllAsync();
            LocationList.OrderByDescending(x => x.Id).ToList();
            return _mapper.Map<IEnumerable<LocationViewModel>>(LocationList).Where(x => x.IsActive != IsActive.Delete);

        }
    }
}