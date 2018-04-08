using System.Collections.Generic;
using Eureka.Spe.AcademicUnits.Dto;
using Eureka.Spe.Notifications.Dto;

namespace Eureka.Spe.Web.Models.NotificationRequest
{
    public partial class LayoutController
    {
        public class ElementsForEntity
        {
            public string EntityName { get; set; }
            public int Id { get; set; }
            public List<AcademicUnitSelectedDto> AcademicUnitsSelected { get; set; }
            public List<NotificationDto> Notifications { get; set; }
        }
    }
}