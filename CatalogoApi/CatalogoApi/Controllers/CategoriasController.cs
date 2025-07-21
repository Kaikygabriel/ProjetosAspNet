using System.Threading.Tasks;
using CatalogoApi.Data;
using CatalogoApi.Filters;
using CatalogoApi.Model;
using CatalogoApi.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private readonly IUnitOfWork _unitOfWork;
        [HttpGet]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            IEnumerable<Categoria> categorias = _unitOfWork.CategoriaRepository.GetAll();
            if (categorias is null)
                return NotFound("A lista de categorias esta vazia");

            return Ok(categorias);
        }

        [HttpGet("{id:int:min(1)}", Name = "obter")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(c=>c.Id==id);
            if (categoria is null)
                return NotFound("Essa categoria não foi encontrada");
            return Ok(categoria);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return Ok(_unitOfWork.CategoriaRepository.GetCategoriasProdutos());
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();
            var resultado = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();
            return CreatedAtRoute("obter", new { categoria.Id }, categoria);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();
            if (categoria is null)
                return NotFound();

            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();
            return Ok(categoria);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(c=>c.Id==id);
            if (categoria is null)
                return NotFound("Categoria não encontrada!");
            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();
            return Ok(categoria);
        }
    }
}
