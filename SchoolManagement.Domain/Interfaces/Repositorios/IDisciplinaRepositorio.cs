using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IDisciplinaRepositorio : IRepositorioBase<Disciplina>
    {
        Disciplina IncluirDisciplina(Disciplina disciplina);

        IEnumerable<Disciplina> FiltroDisciplina(string nomeDisciplina, int LivroId);

        IEnumerable<Disciplina> BuscarPorNome(string nome);

        IEnumerable<Disciplina> RecuperarDisciplinasTurma(int TurmaId);

        bool IncluirDisciplinasEmTurma(int TurmaId, List<Disciplina> ListaDisciplinas);

        bool RemoverDisciplinasTurma(int TurmaId, List<Disciplina> ListaDisciplinas);
    }
}
