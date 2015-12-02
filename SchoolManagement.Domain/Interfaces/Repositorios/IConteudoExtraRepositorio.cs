﻿using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IConteudoExtraRepositorio : IRepositorioBase<ConteudosExtras>
    {
        ConteudosExtras IncluirConteudosExtras(ConteudosExtras conteudosExtras);
        IEnumerable<ConteudosExtras> RecuperarConteudosExtrasTurma(int TurmaId);
        IEnumerable<ConteudosExtras> RecuperarProvasProfessor(int ProfessorId);
    }
}
