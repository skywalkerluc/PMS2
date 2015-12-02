using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.ViewModels
{
    public class DisciplinaViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int DisciplinaId { get; set; }

        [Display(Name = "Nome da disciplina")]
        [Required(ErrorMessage = "O nome da disciplina é necessário para prosseguir.")]
        public string NomeDisciplina { get; set; }

        public virtual ICollection<LivroViewModel> Livros { get; set; }

        public List<int> livrosSelecionados { get; set; }
    }
}