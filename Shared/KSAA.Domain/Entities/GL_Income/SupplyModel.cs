using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Domain.Entities.GL_Income
{
    public class SupplyModel
    {
        public string? GL_Code { get; set; }
        public string? Accounting_voucher { get; set; }
        public string? Amount { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
        public string? HSN_SAC { get; set; }
        public string? CGST { get; set; }
        public string? SGST { get; set; }
        public string? IGST { get; set; }
        public string? CESS { get; set; }
        public string? GSTIN { get; set; }
        public string? Remarks { get; set; }
        public string? Action { get; set; }
    }
}
