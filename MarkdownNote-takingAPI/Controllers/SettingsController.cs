using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownNote_takingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IAccountService _accountService;
        public SettingsController(ISettingsService settingsService, IAccountService accountService)
        {
            _settingsService = settingsService;
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user == null) return BadRequest();

                var settings = await _settingsService.GetByUserId(userId);
                return Ok(settings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{userId}")]
        public async Task<IActionResult> Update(string userId, UpdateSettingsDTO model)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user == null) return BadRequest();

                var settings = await _settingsService.GetByUserId(userId);
                if(settings == null) return BadRequest();

                await _settingsService.Update(settings, model);
                return Ok(new { Message = "Update is succeeded." ,Settings = settings });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
