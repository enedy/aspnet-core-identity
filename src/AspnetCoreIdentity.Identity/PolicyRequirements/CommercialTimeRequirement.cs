using Microsoft.AspNetCore.Authorization;

namespace AspnetCoreIdentity.Identity.PolicyRequirements
{
    public class CommercialTimeRequirement : IAuthorizationRequirement
    {
        public CommercialTimeRequirement() { }
    }
}
