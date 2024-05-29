using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioUsuarioSistemaFinanceiro : RepositoryGenerics<UsuarioSistemaFinanceiro>, InterfaceUsuarioSistemaFinanceiro
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioUsuarioSistemaFinanceiro()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();

        }

        public async Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int IdSistema)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioSistemaFinaceiro
                    .Where(s => s.IdSistema == IdSistema).AsNoTracking()
                    .ToListAsync();

            }
        }

        public async Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioSistemaFinaceiro.AsNoTracking().FirstOrDefaultAsync(x => x.EmailUsuario.Equals(emailUsuario));

            }
        }

        public async Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuarios)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                
                    banco.UsuarioSistemaFinaceiro
                    .RemoveRange(usuarios);

                await banco.SaveChangesAsync();

            }
        }

        public async Task Add(UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                await banco.UsuarioSistemaFinaceiro.AddAsync(usuarioSistemaFinanceiro);
                await banco.SaveChangesAsync();
            }
        }
    }
}
    

