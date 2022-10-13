﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master
{
    public class LocationViewModel
    {
        public long Id { get; set; }
        public string? Location_Code { get; set; }
        public string? Address { get; set; }
        public string? GSTRegistrationNo { get; set; }
        public string? TypeOfUnit { get; set; }
        public string? ProductsManufactured { get; set; }
        public string? ProductsTraded { get; set; }
        public string? TypeOfServicesProvided { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }

    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DatumLocationData
    {
        public long id { get; set; }
        public string? location_Code { get; set; }
        public string? address { get; set; }
        public string? gstregistrationNo { get; set; }
        public string? typeOfUnit { get; set; }
        public string? productsManufactured { get; set; }
        public string? productsTraded { get; set; }
        public string? typeofservicesProvided { get; set; }
        public string? iP { get; set; }
        public string? browserCase { get; set; }
        public IsActive isActive { get; set; }
    }

    public class RootLocation
    {
        public List<DatumLocationData> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}