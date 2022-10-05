using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master
{
    public class TaxCodeViewModel
    {
        public long Id { get; set; }
        public string? Tax_Code { get; set; }
        public string? SubtaxName { get; set; }
        public int TaxRate { get; set; }
        public string? Type { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DatumTaxCode
    {
        public long id { get; set; }
        public string? tax_Code { get; set; }
        public string? subtaxName { get; set; }
        public int taxRate { get; set; }
        public string? type { get; set; }
        public string? iP { get; set; }
        public string? browserCase { get; set; }
        public IsActive isActive { get; set; }
    }

    public class RootTaxCode
    {
        public List<DatumTaxCode> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}