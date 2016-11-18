using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface ITrabalhosExtrasRepositorio : IRepositorioBase<TrabalhosExtras>
    {
        bool IncluirTrabalhoExtra(TrabalhosExtras trabalhoExtra);
        bool LancarNota(Aluno aluno, Professor professor, TrabalhosExtras trab, Prova prova, int nota);
        IEnumerable<TrabalhosExtras> RecuperarTrabalhosTurma(int TurmaId);
    }
}
