using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface ITrabalhosExtrasRepositorio : IRepositorioBase<TrabalhosExtras>
    {
        bool LancarNota(Aluno aluno, Professor professor, TrabalhosExtras trab, Prova prova, int nota);
        IEnumerable<TrabalhosExtras> RecuperarTrabalhosTurma(int TurmaId);
    }
}
