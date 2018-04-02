using System.Collections.Generic;
using Eureka.Spe.AcademicUnits.Dto;

namespace Eureka.Spe.AcademicUnits.helpers
{
    public class AcademicUnitComparer : IEqualityComparer<AcademicUnitDto>
    {
        public bool Equals(AcademicUnitDto x, AcademicUnitDto y)
        {
            //Check whether the objects are the same object. 
            if (ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.Id.Equals(y.Id);
        }

        public int GetHashCode(AcademicUnitDto obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = obj.Id.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }
}
