using System.Collections.Generic;
using Eureka.Spe.Roles.Dto;
using Eureka.Spe.Users.Dto;

namespace Eureka.Spe.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}