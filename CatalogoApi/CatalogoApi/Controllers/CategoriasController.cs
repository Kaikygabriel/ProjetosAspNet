using System.Threading.Tasks;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Filters;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            IEnumerable<Categoria> categorias = _unitOfWork.CategoriaRepository.GetAll();
            if (categorias is null)
                return NotFound("A lista de categorias esta vazia");
            IEnumerable<CategoriaDTO> categoriasDto = categorias.ToCategoriaDTOList();
            return Ok(categoriasDto);
        }

        [HttpGet("{id:int:min(1)}", Name = "obter")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(c=>c.Id==id);
            if (categoria is null)
                return NotFound("Essa categoria não foi encontrada");
            CategoriaDTO categoriaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return Ok(_unitOfWork.CategoriaRepository.GetCategoriasProdutos());
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return NotFound();

            Categoria categoria = categoriaDto.ToCategoria();
           
            _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();
            return CreatedAtRoute("obter", new { categoriaDto.Id }, categoriaDto);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            
            if (id != categoriaDto.Id)
                return BadRequest();
            if (categoriaDto is null)
                return NotFound();

            Categoria categoria = categoriaDto.ToCategoria();

            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();
            return Ok(categoriaDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(c=>c.Id==id);
            if (categoria is null)
                return NotFound("Categoria não encontrada!");
            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();
            CategoriaDTO? categoriaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }
    }
}
