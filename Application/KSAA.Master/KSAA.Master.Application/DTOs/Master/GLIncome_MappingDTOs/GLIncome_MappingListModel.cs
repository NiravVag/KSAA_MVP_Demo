﻿using KSAA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application.DTOs.Master.GLIncome_MappingDTOs
{
    public class GLIncome_MappingListModel
    {
        public long Id { get; set; }
        public string? GLIncomeCode { get; set; }
        public string? GLIncomeDescription { get; set; }
        public string? IP { get; set; }
        public string? BrowserCase { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IsActive IsActive { get; set; }
    }
}
