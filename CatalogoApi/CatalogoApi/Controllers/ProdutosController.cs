using AutoMapper;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Model.DTO;
using CatalogoApi.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get() 
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
                return NotFound("Produtos não encontrado...");
            return Ok(produtos.ToProdutosDTOList());
        }
         
        [HttpGet("{id:int:min(1)}",Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            var produtoDTO = produto.ToProdutoDTO();
            return Ok(produtoDTO);
        }

        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDto)
        {
            var produto = produtoDto.ToProduto();
            if (produto is null)
                return BadRequest("Produto nulo");
            _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("ObterProduto", new { produto.Id}, produto);
        }
        [HttpPatch("{id:int:min(1)}/updatePartial")]
        public ActionResult<ProdutoDTOUpdateResponse>Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
        {
            if (patchProdutoDto is null)
                return BadRequest();

            var produto = _unitOfWork.ProdutoRepository.GetById(x => x.Id == id);

            if (produto is null)
                return NotFound();

            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
                return BadRequest(ModelState);

            _mapper.Map(produtoUpdateRequest, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();
            return Ok(produto.ToProdutoDTOUpdateResponse());
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, ProdutoDTO produtoDto)
        {
            var produto = produtoDto.ToProduto();

            if (id != produto.Id)
                return BadRequest();

            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();
            return Ok(produtoDto);
        }
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(p=>p.Id==id);
            if (produto is null)
                return NotFound("Produto não Encontrado...");
            var produtoExcluido= _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            return Ok(produtoExcluido.ToProdutoDTO());
        }
    }
}
