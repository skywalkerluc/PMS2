using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IDisciplinaServico : IServicoBase<Disciplina>
    {
        Disciplina IncluirDisciplina(Disciplina disciplina);

        IEnumerable<Disciplina> FiltroDisciplina(string nomeDisciplina, int LivroId);

        IEnumerable<Disciplina> BuscarPorNome(string nome);

        IEnumerable<Disciplina> RecuperarDisciplinasTurma(int TurmaId);

        bool IncluirDisciplinasEmTurma(int TurmaId, List<Disciplina> ListaDisciplinas);

        bool RemoverDisciplinasTurma(int TurmaId, List<Disciplina> ListaDisciplinas);

        IEnumerable<Disciplina> RecuperarDisciplinasProfessorLeciona(int ProfessorId);

        IEnumerable<Disciplina> RecuperarDisciplinasTurmaProfessor(int TurmaId, int ProfessorId);
    }
}
