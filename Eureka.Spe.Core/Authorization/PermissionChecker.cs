using Abp.Authorization;
using Eureka.Spe.Authorization.Roles;
using Eureka.Spe.Authorization.Users;

namespace Eureka.Spe.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
