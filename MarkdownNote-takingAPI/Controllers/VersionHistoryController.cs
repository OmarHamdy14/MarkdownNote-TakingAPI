using MarkdownNote_takingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownNote_takingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionHistoryController : ControllerBase
    {
        private readonly IVersionHistoryService _versionHistoryService;
        private readonly INoteService _noteService;
        public VersionHistoryController(IVersionHistoryService versionHistoryService, INoteService noteService)
        {
            _versionHistoryService = versionHistoryService;
            _noteService = noteService;
        }
        [Authorize]
        [HttpGet("GetById/{VId}")]
        public async Task<IActionResult> GetById(int VId)
        {
            if (VId <= 0) return BadRequest();
            try
            {
                var version = await _versionHistoryService.GetById(VId);
                if (version is null) return NotFound();

                return Ok(version);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllVersionsbyNoteId/{noteId}")]
        public async Task<IActionResult> GetAllVersionsbyNoteId(int noteId)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if (note == null) return NotFound();

                var versions = await _versionHistoryService.GetAllVersionsbyNoteId(noteId);
                return Ok(versions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("SaveVersion/{noteId}")]
        public async Task<IActionResult> SaveVersion(int noteId)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if (note == null) return NotFound();

                await _versionHistoryService.SaveVersion(note);
                return Ok(new {Message = "Version is saved."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
