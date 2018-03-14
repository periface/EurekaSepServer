using System.Collections.Generic;
using Eureka.Spe.Roles.Dto;

namespace Eureka.Spe.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}