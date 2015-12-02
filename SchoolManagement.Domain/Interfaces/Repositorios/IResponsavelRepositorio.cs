using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IResponsavelRepositorio : IRepositorioBase<Responsavel>
    {
        IEnumerable<Responsavel> PesquisarResponsavelPorNome(string nomeResponsavel);
        IEnumerable<Aluno> ExibirDadosAlunoRelacionado(int responsavelId);
        IEnumerable<Responsavel> FiltrarResponsavel(string nomeResponsavel, int idAluno);
    }
}
