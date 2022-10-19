﻿using KSAA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master.PlantCodeDTOs
{
    public class PlantCodeListModel
    {
        public long Id { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Plant_Code { get; set; }
        public string? GSTRegistrationNo { get; set; }
        public string? TypeOfUnit { get; set; }
        public string? ProductsManufactured { get; set; }
        public string? ProductsTraded { get; set; }
        public string? ServicesProvided { get; set; }
        public string? RegistrationType { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }
}
