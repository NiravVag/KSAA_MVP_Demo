using KSAA.Master.Application.DTOs.Master.CompanyDTOs;
using KSAA.Master.Application.Features.Master.Commands.CompanyCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyViewModel> AddCompany(CreateCompanyCommand command);

        Task<CompanyViewModel> EditCompany(UpdateCompanyCommand command);

        Task<IEnumerable<CompanyViewModel>> GetCompanyList();

        Task<CompanyViewModel> GetCompanyById(long id);

        Task DeleteCompany(DeleteCompanyCommand command);
    }
}
