using Entities.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Configuracao
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {

        public ContextBase(DbContextOptions options) : base(options)
        {
        
        
        }


        public DbSet<SistemaFinanceiro> SistemaFinanceiro { get; set; }
        public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinaceiro { get; set; }

        public DbSet<Categoria> Categoria { get; set;}
        public DbSet<Despesa> Despesa { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);

            }
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);


            // Configuração das propriedades DateTime da entidade Despesa
            builder.Entity<Despesa>(entity =>
            {
                entity.Property(e => e.DataCadastro)
                .HasColumnType("datetime")
                .IsRequired();

                entity.Property(e => e.DataAlteracao)
                 .HasColumnType("datetime")
                 .IsRequired();

                entity.Property(e => e.DataPagamento)
                 .HasColumnType("datetime")
                 .IsRequired();

                entity.Property(e => e.DataVencimento)
                 .HasColumnType("datetime")
                 .IsRequired();
            });


            // Configuração das propriedades DateTime da entidade despesa


            base.OnModelCreating(builder);
        }

        public string ObterStringConexao()
        {
            return "server=localhost;initial catalog=Financeiro_2024;uid=root;pwd=1234";

        }
        

    }
}
