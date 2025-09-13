using System.Linq.Expressions;
using CatalogoApi.Model;
using CatalogoApi.Pagination;
using CatalogoApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApiTests.Mocks;

public class FakeProdutoRepository : IRepositoryProduto
{

List<Produto> ProdutosFake = new List<Produto>
{
    new Produto(
        id: 1,
        nome: "Notebook Dell Inspiron",
        descricao: "Notebook Dell Inspiron 15 polegadas, 16GB RAM, SSD 512GB.",
        preco: 4500.99m,
        imagemUrl: "notebook_dell.jpg",
        estoque: 25,
        dataCadastro: DateTime.Now.AddDays(-30),
        categoriaId: 1,
        categoria: new Categoria { Id = 1, Nome = "Informática" }
    ),
    new Produto(
        id: 2,
        nome: "Smartphone Samsung Galaxy",
        descricao: "Smartphone Samsung Galaxy S22, 128GB, câmera tripla.",
        preco: 3500.00m,
        imagemUrl: "samsung_galaxy.jpg",
        estoque: 40,
        dataCadastro: DateTime.Now.AddDays(-20),
        categoriaId: 2,
        categoria: new Categoria { Id = 2, Nome = "Celulares" }
    ),
    new Produto(
        id: 3,
        nome: "Cadeira Gamer Redragon",
        descricao: "Cadeira Gamer Redragon com apoio lombar e ajuste de altura.",
        preco: 1200.50m,
        imagemUrl: "cadeira_gamer.jpg",
        estoque: 15,
        dataCadastro: DateTime.Now.AddDays(-10),
        categoriaId: 3,
        categoria: new Categoria { Id = 3, Nome = "Móveis" }
    ),
    new Produto(
        id: 4,
        nome: "Teclado Mecânico HyperX",
        descricao: "Teclado mecânico HyperX com switches Red, iluminação RGB.",
        preco: 650.75m,
        imagemUrl: "teclado_hyperx.jpg",
        estoque: 50,
        dataCadastro: DateTime.Now.AddDays(-5),
        categoriaId: 1,
        categoria: new Categoria { Id = 1, Nome = "Informática" }
    ),
    new Produto(
        id: 5,
        nome: "Monitor LG UltraWide",
        descricao: "Monitor LG 29'' UltraWide Full HD IPS.",
        preco: 1800.00m,
        imagemUrl: "monitor_lg.jpg",
        estoque: 10,
        dataCadastro: DateTime.Now,
        categoriaId: 1,
        categoria: new Categoria { Id = 1, Nome = "Informática" }
    )
};
    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        await Task.Delay(0);
        return ProdutosFake;
    }

    public async Task<Produto?> GetByIdAsync(Expression<Func<Produto, bool>> predicate)
    {
        await Task.Delay(0);
        return  ProdutosFake.AsQueryable().FirstOrDefault(predicate);
    }

    public Produto Create(Produto entity)
    {
        ProdutosFake.Add(entity);
        return entity;
    }

    public Produto Update(Produto entity)
    {
        ProdutosFake.Add(entity);
        return entity;
    }

    public Produto Delete(Produto entity)
    {
        ProdutosFake.Remove(entity);
        return entity;
    }

    public async Task<PagedList<Produto>> GetAllProductAsync(ProdutosPagination pagination)
    {
        await Task.Delay(0);
        return PagedList<Produto>.ToPagedList(ProdutosFake,pagination.PageNumber,pagination.PageSize);
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltro)
    {
        await Task.Delay(0);
        return PagedList<Produto>.ToPagedList(ProdutosFake,produtosFiltro.PageNumber,produtosFiltro.PageSize);
    }
}