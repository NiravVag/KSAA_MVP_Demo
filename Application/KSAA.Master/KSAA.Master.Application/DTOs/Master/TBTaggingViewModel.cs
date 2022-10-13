using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master
{
    public class TBTaggingViewModel
    {
        public long Id { get; set; }
        public string? TBTaggingCode { get; set; }
        public string? GLCode { get; set; }
        public string? GLName { get; set; }
        public decimal Amount { get; set; }
        public string? TagCode { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DatumTBTagging
    {
        public long id { get; set; }
        public string? tbTaggingCode { get; set; }
        public string? glCode { get; set; }
        public string? glName { get; set; }
        public decimal amount { get; set; }
        public string? tagCode { get; set; }
        public string? iP { get; set; }
        public string? browserCase { get; set; }
        public IsActive isActive { get; set; }
    }

    public class RootTBTagging
    {
        public List<DatumTBTagging> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}