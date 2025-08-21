using System.Text.Json.Serialization;
using BlibiotecaApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlibiotecaApi.Data;

public class BlibiotecaContextApi : IdentityDbContext
{
    public BlibiotecaContextApi(DbContextOptions<BlibiotecaContextApi>options) : base(options)
    {
        
    }

    public DbSet<Livro>Livros { get; set; }
    [JsonIgnore]
    public DbSet<Blibioteca>Blbiotecas{ get; set; }
}