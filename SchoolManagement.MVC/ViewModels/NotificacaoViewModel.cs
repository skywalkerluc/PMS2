using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class NotificacaoViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int NotificacaoId { get; set; }

        [Required(ErrorMessage = "Assunto é um campo necessário para prosseguir.")]
        public string Assunto { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data de criação da notificação")]
        [ScaffoldColumn(false)]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Usuário de criação")]
        public UsuarioViewModel UsuarioCriacao { get; set; }
        
        [Display(Name = "Turma (Público-alvo)")]
        public TurmaViewModel TurmaPublicoAlvo { get; set; }

        [Display(Name = "Professores (Público-alvo)")]
        public ICollection<ProfessorViewModel> ProfessoresPublicoAlvo { get; set; }

        [Display(Name = "Professor")]
        public List<SelectListItem> ListaProfessores { get; set; }

        [Display(Name = "Turma")]
        public List<SelectListItem> ListaTurmas { get; set; }

        public int turmaEscolhida { get; set; }
        
    }
}