using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats.Dto
{
    [AutoMap(typeof(MetricElement))]
    public class MetricDto : FullAuditedEntityDto
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }

        public int? StudentId { get; set; }

        public string CategoryEntityName { get; set; }
        public int CategoryEntityId { get; set; }

        public int Note { get; set; }
        public string Comment { get; set; }
    }
}
