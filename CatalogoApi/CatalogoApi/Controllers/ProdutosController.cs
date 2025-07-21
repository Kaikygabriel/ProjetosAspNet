using CatalogoApi.Data;
using CatalogoApi.Model;
using CatalogoApi.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() 
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
                return NotFound("Produtos não encontrado...");
            return Ok(produtos);
        }
         
        [HttpGet("{id:int:min(1)}",Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest("Produto nulo");
            _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("ObterProduto", new {produto.Id},produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();
            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            var produtoExcluido= _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            return Ok(produtoExcluido);
        }
    }
}
