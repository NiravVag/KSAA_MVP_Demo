using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Features.Master.Commands.VendorCodeCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.Interfaces.Services
{
    public interface IVendorCodeService
    {
        Task<VendorCodeViewModel> AddVendorCode(CreateVendorCodeCommand command);

        Task<VendorCodeViewModel> EditVendorCode(UpdateVendorCodeCommand command);

        Task<List<VendorCodeViewModel>> GetVendorCodeList();

        Task<VendorCodeViewModel> GetVendorCodeById(long id);

        Task DeleteVendorCode(DeleteVendorCodeCommand command);
    }
}
