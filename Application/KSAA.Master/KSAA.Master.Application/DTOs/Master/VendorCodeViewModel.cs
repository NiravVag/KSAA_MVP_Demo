using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master
{
    public class VendorCodeViewModel
    {
        public long Id { get; set; }
        public string? Customer_Code { get; set; }
        public string? GSTN { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DatumVendorCode
    {
        public long id { get; set; }
        public string? customer_Code { get; set; }
        public string? gstn { get; set; }
        public string? name { get; set; }
        public string? location { get; set; }
        public string? address { get; set; }
        public string iP { get; set; }
        public string browserCase { get; set; }
        public IsActive isActive { get; set; }
    }

    public class RootVendorCode
    {
        public List<DatumVendorCode> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}