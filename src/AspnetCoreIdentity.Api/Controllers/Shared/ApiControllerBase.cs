using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreIdentity.Api.Controllers.Shared
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiControllerBase : ControllerBase { }
}
