using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspnetCoreIdentity.Identity.PolicyRequirements
{
    public class CommercialTimeHandler : AuthorizationHandler<CommercialTimeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommercialTimeRequirement requirement)
        {
            var horarioAtual = TimeOnly.FromDateTime(DateTime.Now);
            if (horarioAtual.Hour >= 8 && horarioAtual.Hour <= 18)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
