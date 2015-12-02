﻿using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface ITurmaServico : IServicoBase<Turma>
    {
        Turma IncluirTurma(Turma turma);
        IEnumerable<Aluno> RecuperarTodosAlunosTurma(int TurmaId);
        IEnumerable<Turma> FiltrarTurma(string descTurma, Professor professor, AnoLetivo ano, int horarioId);
        IEnumerable<ResultadosProvas> RecuperarResultadosProvasTurma(int TurmaId);
    }
}
