﻿using KSAA.Master.Application.DTOs.Master.LocationDTOs;
using KSAA.Master.Application.Features.Master.Commands.LocationCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.Interfaces.Services
{
    public interface ILocationService
    {
        Task<LocationViewModel> AddLocation(CreateLocationCommand command);

        Task<LocationViewModel> EditLocation(UpdateLocationCommand command);

        Task<IEnumerable<LocationViewModel>> GetLocationList();

        Task<LocationViewModel> GetLocationById(long id);

        Task DeleteLocation(DeleteLocationCommand command);
    }
}
