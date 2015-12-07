using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.ViewModels
{
    public class ConteudosExtrasViewModel
    {
        public int ConteudoId { get; set; }

        public DateTime DataHoraCriacao { get; set; }
        public ProfessorViewModel Professor { get; set; }
        public string Anexo { get; set; }
        public TurmaViewModel TurmaPublicoAlvo { get; set; }
        public string Descricao { get; set; }

        public List<SelectListItem> ListaTurmas { get; set; }
        public int TurmaSelecionada { get; set; }

        [Display(Name = "Tipo de Conteudo")]
        public String tipoConteudo { get; set; }
        public int tipoConteudoEscolhido { get; set; }

        public int turmaSelecionada { get; set; }
    }
}