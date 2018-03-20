using System.Collections.Generic;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Contracts
{
    public interface IHasAcademicUnits
    {
        ICollection<AcademicUnit> AcademicUnits { get; set; }
    }
}