﻿using KSAA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master.CompanyDTOs
{
    public class CompanyViewModel
    {
        public long Id { get; set; }
        public string? Company_Code { get; set; }
        public string? Company_Name { get; set; }
        public string? Company_Address { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }
}