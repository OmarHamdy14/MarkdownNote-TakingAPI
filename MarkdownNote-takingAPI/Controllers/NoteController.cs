using MarkdownNote_takingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownNote_takingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        public NoteController(INoteService noteService, IAccountService accountService, ICategoryService categoryService)
        {
            _noteService = noteService;
            _accountService = accountService;
            _categoryService = categoryService;
        }
        [Authorize]
        [HttpGet("GetById/{noteId}")]
        public async Task<IActionResult> GetById(int noteId)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId,"Files");
                if (note is null) return NotFound();

                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllNotesByUserId/{userId}")]
        public async Task<IActionResult> GetAllNotesByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user is null) return NotFound();

                var notes = await _noteService.GetAllNotesByUserId(userId, "Files");
                //if (!notes.Any()) return NotFound();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllNotesByUserIdAndCategoryId/{userId}/{categoryId}")]
        public async Task<IActionResult> GetAllNotesByUserIdAndCategoryId(string userId, int categoryId)
        {
            if (string.IsNullOrEmpty(userId) || categoryId <= 0) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user is null) return NotFound();
                var category = await _categoryService.GetById(categoryId);
                if(category is null) return NotFound();

                var notes = await _noteService.GetAllNotesByUserIdAndCategoryId(userId, categoryId, "Files");
                //if (!notes.Any()) return NotFound();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("CheckGrammer/{content}")]
        public async Task<IActionResult> CheckGrammer(string content)
        {
            if(string.IsNullOrEmpty(content)) return BadRequest();
            try
            {
                var result = await _noteService.CheckGrammer(content);
                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize] 
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateNoteDTO model)
        {
            try
            {
                var note = await _noteService.Create(model);
                return CreatedAtAction("GetAllNotesByUserIdAndCategoryId", new { userId = note.UserId, categoryId = note.CategoryId }, note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("UploadFiles/{noteId}")]
        public async Task<IActionResult> UploadFiles(int noteId,[FromBody]IFormFile file)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if (note == null) return NotFound();

                if (file == null || file.Length == 0) return BadRequest();
                var fileUrl = await _noteService.UploadFiles(note, file);
                if(fileUrl != null) return Ok(new {FileUrl = fileUrl});
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{noteId}")]
        public async Task<IActionResult> Update(int noteId, [FromBody]UpdateNoteDTO model)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if (note == null) return NotFound();

                await _noteService.Update(note, model);
                return Ok(new { Message = "Update is succeeded", Note = note });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("AutoSave/{noteId}")]
        public async Task<IActionResult> AutoSave(int noteId, [FromBody]UpdateNoteDTO model)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if (note == null) return NotFound();

                await _noteService.Update(note, model);
                return Ok(new { Message = "Auto Save is succeeded", Note = note });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("Delete/{noteId}")]
        public async Task<IActionResult> Delete(int noteId)
        {
            if (noteId <= 0) return BadRequest();
            try
            {
                var note = await _noteService.GetById(noteId);
                if(note == null) return NotFound();

                await _noteService.Delete(note);
                return Ok(new { Message = "Deletion is succeeded." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("DeleteFile/{FileId}")]
        public async Task<IActionResult> DeleteFile(int FileId)
        {
            try
            {
                var file = await _noteService.GetFileById(FileId);
                if(file == null) return NotFound();

                await _noteService.DeleteFile(file);
                return Ok(new { Message = "Deletion is done." });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
