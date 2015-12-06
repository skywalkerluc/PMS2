using SchoolManagement.Data.EntityConfig;
using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Contexto
{
    public class SchoolManagementContext : DbContext
    {
        public SchoolManagementContext()
            : base("SchoolManagementDB")
        {

        }

        #region Coleções de Entidades
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AnoLetivo> AnosLetivos { get; set; }
        public DbSet<Boleto> Boletos { get; set; }
        public DbSet<ConteudosExtras> ConteudosExtras { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<FaleConosco> ContatosFaleConosco { get; set; }
        public DbSet<Frequencia> Frequencia { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LojasRecomendadas> LojasRecomendadas { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Prova> Provas { get; set; }
        public DbSet<Rematricula> Rematriculas { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<ResultadosProvas> ResultadosProvas { get; set; }
        public DbSet<TrabalhosExtras> TrabalhosExtras { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<Endereco>();
            modelBuilder.ComplexType<Contato>();

            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());
            modelBuilder.Properties<string>().
                Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().
                Configure(p => p.HasMaxLength(100));
            

            #region Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<MappingInheritedPropertiesSupportConvention>();
            
            #endregion

            #region Configurations
            modelBuilder.Configurations.Add(new AlunoConfig());
            modelBuilder.Configurations.Add(new ProfessorConfig());
            modelBuilder.Configurations.Add(new FuncionarioConfig());
            modelBuilder.Configurations.Add(new ResponsavelConfig());
            modelBuilder.Configurations.Add(new LojasRecomendadasConfig());
            modelBuilder.Configurations.Add(new ProvasConfig());
            modelBuilder.Configurations.Add(new ResultadosProvasConfig());
            modelBuilder.Configurations.Add(new RematriculaConfig());
            modelBuilder.Configurations.Add(new NotificacaoConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());

            //modelBuilder.Entity<Aluno>().MapToStoredProcedures();
            #endregion

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Ano") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Ano").CurrentValue = DateTime.Now.Year;
                }
            }
            return base.SaveChanges();
        }
    }
}
