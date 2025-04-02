using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownNote_takingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationService _collaborationService;
        private readonly INoteService _noteService;
        public CollaborationController(ICollaborationService collaborationService, INoteService noteService)
        {
            _collaborationService = collaborationService;
            _noteService = noteService;
        }
        [Authorize]
        [HttpGet("GetById/{CollaborationId}")]
        public async Task<IActionResult> GetById(int CollaborationId)
        {
            if (CollaborationId <= 0) return BadRequest();
            try
            {
                var collab = await _collaborationService.GetById(CollaborationId);
                if (collab is null) return NotFound();

                return Ok(collab);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllCollaborationsByNoteId/{noteId}")]
        public async Task<IActionResult> GetAllCollaborationsByNoteId(int noteId)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if (note is null) return NotFound();

                var collabs = await _collaborationService.GetAllCollaborationsByNoteId(noteId);
                return Ok(collabs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateCollaborationDTO model)
        {
            try
            {
                var collab = await _collaborationService.Create(model);
                return CreatedAtAction("GetAllCollaborationsByNoteId", new { noteId = collab.NoteId }, collab);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{CollaborationId}")]
        public async Task<IActionResult> Update(int CollaborationId, [FromBody]UpdateCollaborationDTO model)
        {
            if (CollaborationId <= 0) return BadRequest();
            try
            {
                var collab = await _collaborationService.GetById(CollaborationId);
                if(collab == null) return BadRequest();

                await _collaborationService.Update(collab, model);
                return Ok(new { Message = "Update is succeeded.", Collaboration = collab });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }

        }
        [Authorize]
        [HttpDelete("Delete/{CollaborationId}")]
        public async Task<IActionResult> Delete(int CollaborationId)
        {
            if (CollaborationId <= 0) return BadRequest();
            try
            {
                var collab = await _collaborationService.GetById(CollaborationId);
                if (collab == null) return BadRequest();

                await _collaborationService.Delete(collab);
                return Ok(new { Message = "Deletion is succeeded." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
