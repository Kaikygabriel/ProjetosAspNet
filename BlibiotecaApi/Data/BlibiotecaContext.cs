using BlibiotecaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Data;

public class BlibiotecaContext : DbContext
{
    public BlibiotecaContext(DbContextOptions<BlibiotecaContext>options):base(options)
    {
        
    }

    public DbSet<Livro>Livros { get; set; }
}