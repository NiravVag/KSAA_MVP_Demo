﻿using AutoMapper;
using KSAA.Domain.Entities;
using KSAA.Master.Application.DTOs.Master;
using KSAA.Master.Application.Features.Master.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Application
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CreateDocumentTypeCommand, DocumentType>();
            CreateMap<UpdateDocumentTypeCommand, DocumentType>();
            CreateMap<DocumentType, DocumentTypeViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
        }
    }
}