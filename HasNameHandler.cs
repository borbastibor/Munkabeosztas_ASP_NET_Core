using Microsoft.AspNetCore.Authorization;
using Munkabeosztas_ASP_NET_Core.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core
{
    public class HasNameHandler : AuthorizationHandler<HasNameRequirement>
    {
        private readonly MunkabeosztasDbContext _context;

        public HasNameHandler(MunkabeosztasDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasNameRequirement requirement)
        {
            var name = context.User.Identity.Name.Split('\\').Last();
            //var name = context.User.Identity.Name;
            bool isAdmin = _context.Adminusers.Any(u => u.Username == name);
            if (name != null && name != "" && isAdmin)
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
