using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core
{
    public class HasNameHandler : AuthorizationHandler<HasNameRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasNameRequirement requirement)
        {
            var name = context.User.Identity.Name;
            if (name != null && name != "")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
