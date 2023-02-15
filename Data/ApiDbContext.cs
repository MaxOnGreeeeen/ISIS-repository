using Microsoft.EntityFrameworkCore;
using NotesWebApp.Models;

namespace NotesWebApp.Data;

public class ApiDbContext: DbContext
{ 
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
    :base(options)
    {

    }

    public DbSet<Note> Notes {get; set;}
    
}