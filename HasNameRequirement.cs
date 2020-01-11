using Microsoft.AspNetCore.Authorization;

namespace Munkabeosztas_ASP_NET_Core
{
    public class HasNameRequirement : IAuthorizationRequirement
    {
        public HasNameRequirement() { }
    }
}
