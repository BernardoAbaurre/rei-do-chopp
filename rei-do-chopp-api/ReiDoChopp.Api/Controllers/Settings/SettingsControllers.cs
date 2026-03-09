using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.Settings.Services;
using ReiDoChopp.DataTransfer.Settings;

namespace ReiDoChopp.API.Controllers.Settings
{
    [ApiController]
    [Authorize(Policy="Administrador")]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsAppService settingsAppService;

        public SettingsController(ISettingsAppService settingsAppService)
        {
            this.settingsAppService = settingsAppService;
        }

        [HttpPut("stock-reset")]
        public async Task<ActionResult> ResetStocking([FromBody] SettingsAuthRequest request)
        {
            await settingsAppService.ResetStockingHistory(request);
            return Ok();
        }

        [HttpPut("base-reset")]
        public async Task<ActionResult> ResetBase([FromBody] SettingsAuthRequest request)
        {
            await settingsAppService.ResetBase(request);
            return Ok();
        }
    }
}
