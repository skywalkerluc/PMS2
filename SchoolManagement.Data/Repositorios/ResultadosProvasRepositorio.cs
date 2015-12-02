using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class ResultadosProvasRepositorio : RepositorioBase<ResultadosProvas>, IResultadosProvasRepositorio
    {
        public IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId)
        {
            var result = from n in Db.ResultadosProvas
                         where n.Aluno.Id.Equals(AlunoId)
                         select n;

            return result;
        }
    }
}
