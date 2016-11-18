using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IFuncionarioServico : IServicoBase<Funcionario>
    {
        IEnumerable<Funcionario> BuscarPorNome(string nome);
        IEnumerable<Funcionario> FiltrarFuncionario(string funcao, string nome);
    }
}
