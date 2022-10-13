using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master
{
    public class CompanyViewModel
    {
        public long Id { get; set; }
        public string? Company_Code { get; set; }
        public string? Company_Name { get; set; }
        public string? Company_Address { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }
    public class DatumCompany
    {
        public long id { get; set; }
        public string? company_Code { get; set; }
        public string? company_Name { get; set; }
        public string? company_Address { get; set; }
        public string iP { get; set; }
        public string browserCase { get; set; }
        public IsActive isActive { get; set; }
    }

    public class RootCompany
    {
        public List<DatumCompany> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}