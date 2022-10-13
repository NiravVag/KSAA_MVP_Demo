using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master
{
    public class DocumentTypeViewModel
    {
        public long Id { get; set; }
        public string? BillType { get; set; }
        public string? Document_Code { get; set; }
        public string? Document_Type { get; set; }
        public string? OurSoftwareProcessing { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }
    public enum IsActive
    {
        Active = 1,
        DeActive = 2,
        Delete = 3
    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public long id { get; set; }
        public string billType { get; set; }
        public string document_Code { get; set; }
        public string Document_Type { get; set; }
        public string ourSoftwareProcessing { get; set; }
        public string iP { get; set; }
        public string browserCase { get; set; }
        public IsActive isActive { get; set; }
    }

    public class Root
    {
        public List<Datum> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}