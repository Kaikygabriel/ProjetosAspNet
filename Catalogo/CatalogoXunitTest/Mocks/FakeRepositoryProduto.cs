using System.Linq.Expressions;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;

namespace CatalogoXunitTest.Mocks;

public class FakeRepositoryProduto : IRepositoryProduto
{
    public List<Produto> Produtos = new()
    {

        new Produto(1,"Smartphone", "Smartphone com excelente câmera e bateria duradoura",
            "https://example.com/imagens/smartphone.jpg", 25, 2300.00m),

        new Produto(2,"Headset Gamer", "Headset com som 7.1 e microfone com redução de ruído",
            "https://example.com/imagens/headset.jpg", 15, 450.00m),

        new Produto(3,"Monitor 27''", "Monitor Full HD 144Hz para games e produtividade",
            "https://example.com/imagens/monitor.jpg", 8, 1300.00m),

        new Produto(4,"Teclado Mecânico", "Teclado mecânico RGB com switches azuis",
            "https://example.com/imagens/teclado.jpg", 20, 350.00m),

        new Produto(5,"Mouse Gamer", "Mouse com 12000 DPI e iluminação RGB",
            "https://example.com/imagens/mouse.jpg", 30, 220.00m)
    }; 
    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        await Task.Delay(0);
        return Produtos;
    }

    public async Task<Produto?> GetByPredicate(Expression<Func<Produto, bool>> predicate)
    {
        await Task.Delay(0);
        return Produtos.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(Produto entity)
    {
        Produtos.Add(entity);
    }

    public void Update(Produto entity)
    {
        Produtos.Add(entity);
    }

    public void Delete(Produto entity)
    {
        Produtos.Remove(entity);
    }
}