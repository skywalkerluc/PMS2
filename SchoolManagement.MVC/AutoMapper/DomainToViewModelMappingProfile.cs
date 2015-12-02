using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.MVC.ViewModels;

namespace SchoolManagement.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
        
        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioViewModel, Usuario>();  
            Mapper.CreateMap<AlunoViewModel, Aluno>();
            Mapper.CreateMap<AnoLetivoViewModel, AnoLetivo>();
            Mapper.CreateMap<ContatoViewModel, Contato>();
            Mapper.CreateMap<DisciplinaViewModel, Disciplina>();
            Mapper.CreateMap<EnderecoViewModel, Endereco>();
            Mapper.CreateMap<EventoViewModel, Evento>();
            Mapper.CreateMap<ExperienciaViewModel, Experiencia>();
            Mapper.CreateMap<FuncionarioViewModel, Funcionario>();
            Mapper.CreateMap<LojasRecomendadasViewModel, LojasRecomendadas>();
            Mapper.CreateMap<NotificacaoViewModel, Notificacao>();
            Mapper.CreateMap<ProfessorViewModel, Professor>();
            Mapper.CreateMap<ProvaViewModel, Prova>();
            Mapper.CreateMap<RematriculaViewModel, Rematricula>();
            Mapper.CreateMap<ResponsavelViewModel, Responsavel>();
            Mapper.CreateMap<ResultadosProvasViewModel, ResultadosProvas>();
            Mapper.CreateMap<TurmaViewModel, Turma>();
            Mapper.CreateMap<LivroViewModel, Livro>();

        }
    }
}