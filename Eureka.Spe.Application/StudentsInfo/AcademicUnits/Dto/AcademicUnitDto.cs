﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.StudentsInfo.AcademicUnits.Dto
{
    [AutoMap(typeof(AcademicUnit))]
    public class AcademicUnitDto : FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
        public string ShortName { get; set; }
        public bool Selected { get; set; }
    }
}
