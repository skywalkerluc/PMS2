using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
            Mapper.CreateMap<Aluno, AlunoViewModel>();
            Mapper.CreateMap<AnoLetivo, AnoLetivoViewModel>();
            Mapper.CreateMap<Contato, ContatoViewModel>();
            Mapper.CreateMap<Disciplina, DisciplinaViewModel>();
            Mapper.CreateMap<Endereco, EnderecoViewModel>();
            Mapper.CreateMap<Evento, EventoViewModel>();
            Mapper.CreateMap<Experiencia, ExperienciaViewModel>();
            Mapper.CreateMap<Funcionario, FuncionarioViewModel>();
            Mapper.CreateMap<LojasRecomendadas, LojasRecomendadasViewModel>();
            Mapper.CreateMap<Notificacao, NotificacaoViewModel>();
            Mapper.CreateMap<Professor, ProfessorViewModel>();
            Mapper.CreateMap<Prova, ProvaViewModel>();
            Mapper.CreateMap<Rematricula, RematriculaViewModel>();
            Mapper.CreateMap<Responsavel, ResponsavelViewModel>();
            Mapper.CreateMap<ResultadosProvas, ResultadosProvasViewModel>();
            Mapper.CreateMap<Turma, TurmaViewModel>();
            Mapper.CreateMap<Livro, LivroViewModel>();
        }
    }
}