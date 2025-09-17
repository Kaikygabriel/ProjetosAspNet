using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text.Json;
using System.Threading.Tasks;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Filters;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Pagination;
using CatalogoApi.Repository.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CategoriasController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
       
        private ActionResult ObterCategoria(PagedList<Categoria> listCategoriaP)
        {
            var metadata = new
            {
                listCategoriaP.Count,
                listCategoriaP.PageSize,
                listCategoriaP.TotalPages,
                listCategoriaP.CurrentPage,
                listCategoriaP.HasNext,
                listCategoriaP.HasPrevius
            };
            Response.Headers.Append("x-pagination", JsonSerializer.Serialize(metadata));
            var categoriasDTO = listCategoriaP.Adapt<IEnumerable<Categoria>>();
            return Ok(categoriasDTO);
        }

        [HttpGet]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            if (!_cache.TryGetValue("categorias", out IEnumerable<Categoria>? categoriasCache))
            {
                categoriasCache =await _unitOfWork.CategoriaRepository.GetAllAsync();
                var options = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    Size = 1,
                    SlidingExpiration = TimeSpan.FromMinutes(3),
                    Priority = CacheItemPriority.Normal
                };
                _cache.Set("categorias", categoriasCache, options);
            }
            if (categoriasCache is null)
                return NotFound("A lista de categorias esta vazia");
            IEnumerable<CategoriaDTO>? categoriasDto = categoriasCache.ToCategoriaDTOList();
            return Ok(categoriasDto);
        }


        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAsync([FromQuery]CategoriaPagination pagination)
        {
            if (!_cache.TryGetValue("categoriasPagination", out PagedList<Categoria>? categoriasCache))
            {
                categoriasCache = await _unitOfWork.CategoriaRepository.GetAllCategoria(pagination);
                var options = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    Size = 1,
                    SlidingExpiration = TimeSpan.FromMinutes(3),
                    Priority = CacheItemPriority.Normal
                };
                _cache.Set("categoriasPagination", categoriasCache, options);
            }
            //var listCategoriaP = await _unitOfWork.CategoriaRepository.GetAllCategoria(pagination);
            return ObterCategoria(categoriasCache);
        }


        [HttpGet("filters")]
        public async  Task<ActionResult<CategoriaDTO>> GetAsync([FromQuery] CategoriaFiltroName pagination)
        {
            if (!_cache.TryGetValue($"categoriasPaginationFilterName", out PagedList<Categoria>? categoriasCache))
            {
                categoriasCache = await _unitOfWork.CategoriaRepository.GetCategoriaFiltroName(pagination);
                var options = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    Size = 1,
                    SlidingExpiration = TimeSpan.FromMinutes(3),
                    Priority = CacheItemPriority.Normal
                };
                _cache.Set("categoriasPaginationFilterName", categoriasCache, options);
            }
           // var categoria = await _unitOfWork.CategoriaRepository.GetCategoriaFiltroName(pagination);
            return ObterCategoria(categoriasCache);
        }


        [HttpGet("{id:int:min(1)}", Name = "obter")]
        public async Task<ActionResult<CategoriaDTO>> GetAsync(int id)
        {
            var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(c=>c.Id==id);
            if (categoria is null)
                return NotFound("Essa categoria não foi encontrada");
            CategoriaDTO categoriaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync()
        {
            return Ok(await _unitOfWork.CategoriaRepository.GetCategoriasProdutosAsync());
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> PostAsync(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return NotFound();

            Categoria categoria = categoriaDto.ToCategoria();
           
            _unitOfWork.CategoriaRepository.Create(categoria);
            await _unitOfWork.CommitAsync();
            return CreatedAtRoute("obter", new { categoriaDto.Id }, categoriaDto);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<CategoriaDTO>> PutAsync(int id, CategoriaDTO categoriaDto)
        {
            
            if (id != categoriaDto.Id)
                return BadRequest();
            if (categoriaDto is null)
                return NotFound();

            Categoria categoria = categoriaDto.ToCategoria();

            _unitOfWork.CategoriaRepository.Update(categoria);
            await _unitOfWork.CommitAsync();
            return Ok(categoriaDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CategoriaDTO>> DeleteAsync(int id)
        {
            var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(c=>c.Id==id);
            if (categoria is null)
                return NotFound("Categoria não encontrada!");
            _unitOfWork.CategoriaRepository.Delete(categoria);
            await _unitOfWork.CommitAsync();
            CategoriaDTO? categoriaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }
    }
}
    
