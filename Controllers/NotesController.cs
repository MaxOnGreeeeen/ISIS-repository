using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesWebApp.Data;
using NotesWebApp.Models;

namespace NotesWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private static Random random = new Random();

    private readonly ILogger<NotesController> _logger;

    private readonly ApiDbContext _context;

    public NotesController(ILogger<NotesController> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetAllNotes")]     
    public async Task<IActionResult> GetAll()
    {
        await _context.SaveChangesAsync();

        var randomGeneratedValue = random.Next(0, 2);

        if (randomGeneratedValue == 0){
            return BadRequest();
        }

        var allNotes = await _context.Notes.ToListAsync();

        return Ok(allNotes);
    }


    [HttpDelete(Name = "DeleteNote")]
    public async Task<IActionResult> DeleteNote(long id)
    {
        var note = _context.Notes.Where(noteFound => noteFound.Id == id)
            .FirstOrDefault();

        if (note != null)
        {
            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();
        }

        return Ok();
    }

    [HttpPost(Name = "CreateNote")]
    public async Task<IActionResult> PostNote([FromBody] Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPut(Name = "UpdateNote")]
    public async Task<IActionResult> UpdateNote([FromBody] Note note)
    {
        var existingNote = _context.Notes.Where(foundNote => foundNote.Id == note.Id)
                                                  .FirstOrDefault<Note>();

        if (existingNote != null)
        {
            existingNote.Title = note.Title;
            existingNote.Description = note.Description;

            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return Ok();
    }
}
