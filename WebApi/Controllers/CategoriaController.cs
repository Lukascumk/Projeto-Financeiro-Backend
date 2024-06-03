using Domain.Interfaces.ICategoria;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly InterfaceCategoria _InterfaceCategoria;

        private readonly ICategoriaServico _ICategoriaServico;

        public CategoriaController (InterfaceCategoria InterfaceCategoria, 
            ICategoriaServico ICategoriaServico )
        {
            _InterfaceCategoria = InterfaceCategoria;
            _ICategoriaServico = ICategoriaServico;

        }

       [HttpGet("/api/ListarCategoriaUsuario")]
       [Produces("application/json")]
        public async Task<object> ListarCategoriaUsuario(string emailUsuario)
        {
            return _InterfaceCategoria.ListarCategoriasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            await _ICategoriaServico.AdicionarCategoria(categoria);
            return categoria;
        }

        [HttpPut("/api/AtualizarCategoria")]
        [Produces("application/json")]
        public async Task<object> AtualizarCategoria(Categoria categoria)
        {
            await _ICategoriaServico.AtualizarCategoria(categoria);
            return categoria;
        }

        [HttpGet("/api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _InterfaceCategoria.GetEntityById(id);
              
        }

        [HttpDelete("/api/DeleteCategoria")]
        [Produces("application/json")]
        public async Task<object> DeleteCategoria(int id)
        {
            try
            {            
            var categoria = await _InterfaceCategoria.GetEntityById(id);
            await _InterfaceCategoria.Delete(categoria);
            }
            catch (Exception ex) 
            {
                return false;
            }
            return true;
        }
    }
}
