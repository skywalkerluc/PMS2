using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IResponsavelServico : IServicoBase<Responsavel>
    {
        Responsavel IncluirResponsavel(Responsavel responsavel);
        bool CriarRelacaoResponsavelAluno(int ResponsavelId, int AlunoId);
        IEnumerable<Responsavel> PesquisarResponsavelPorNome(string nomeResponsavel);
        IEnumerable<Aluno> ExibirDadosAlunoRelacionado(int responsavelId);
        IEnumerable<Responsavel> FiltrarResponsavel(string nomeResponsavel, int idAluno);
        List<Dictionary<int, string>> ExibirDadosAlunoRelacionado2(int responsavelId);
    }
}
