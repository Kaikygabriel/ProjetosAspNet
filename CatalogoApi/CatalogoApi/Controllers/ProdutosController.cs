 using System.Text.Json;
using AutoMapper;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Model.DTO;
using CatalogoApi.Pagination;
using CatalogoApi.Repository.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private ActionResult ObterProduto(PagedList<Produto> produtos)
        {
            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.TotalPages,
                produtos.CurrentPage,
                produtos.HasNext,
                produtos.HasPrevius
            };
            Response.Headers.Append("x-pagination", JsonSerializer.Serialize(metadata));
            var produtosDto = produtos.Adapt<IEnumerable<ProdutoDTO>>();
            return Ok(produtosDto);
        }

        [HttpGet]
        //[Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync() 
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();
            if (produtos is null)
                return NotFound("Produtos não encontrado...");
            return Ok(produtos!.ToProdutosDTOList());
        }
         
        [HttpGet("{id:int:min(1)}",Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetAsync(int? id)
        {
            if (id <= 0 || id is null)
                return BadRequest();
            Produto? produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            var produtoDTO = produto.ToProdutoDTO();
            return Ok(produtoDTO);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync([FromQuery]ProdutosPagination pagination)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetAllProductAsync(pagination);
            return ObterProduto(produtos);
        }
        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync([FromQuery] ProdutosFiltroPreco pagination)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFiltroPrecoAsync(pagination);
            return ObterProduto(produtos);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(ProdutoDTO produtoDto)
        {
            var produto = produtoDto.ToProduto();
            if (produto is null)
                return BadRequest("Produto nulo");
            _unitOfWork.ProdutoRepository.Create(produto);
            await _unitOfWork.CommitAsync();
            return new CreatedAtRouteResult("ObterProduto", new { produto.Id}, produto);
        }
        [HttpPatch("{id:int:min(1)}/updatePartial")]
        public async Task<ActionResult<ProdutoDTOUpdateResponse>>PatchAsync(int id, 
                                            JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
        {
            if (patchProdutoDto is null)
                return BadRequest();

            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(x => x.Id == id);

            if (produto is null)
                return NotFound();

            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
                return BadRequest(ModelState);

            _mapper.Map(produtoUpdateRequest, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();
            return Ok(produto.ToProdutoDTOUpdateResponse());
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> PutAsync(int id, ProdutoDTO produtoDto)
        {
            var produto = produtoDto.ToProduto();

            if (id != produto.Id)
                return BadRequest();

            _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();
            return Ok(produtoDto);
        }
        [Authorize]
        [HttpDelete("{id:int:min(1)}")] 
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            var produtoExcluido= _unitOfWork.ProdutoRepository.Delete(produto);
            await _unitOfWork.CommitAsync();
            return Ok(produtoExcluido.ToProdutoDTO());
        }
    }
}
