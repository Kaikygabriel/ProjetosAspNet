using BlibiotecaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Data;

public class BlibiotecaContextApi : DbContext
{
    public BlibiotecaContextApi(DbContextOptions<BlibiotecaContextApi>options) : base(options)
    {
        
    }

    public DbSet<Livro>Livros { get; set; }
}