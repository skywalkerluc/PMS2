using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IFuncionarioRepositorio : IRepositorioBase<Funcionario>
    {
        IEnumerable<Funcionario> BuscarPorNome(string nome);
        IEnumerable<Funcionario> FiltrarFuncionario(string funcao, string nome);
    }
}
