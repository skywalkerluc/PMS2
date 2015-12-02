using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IFuncionarioRepositorio : IRepositorioBase<Funcionario>
    {
        IEnumerable<Funcionario> BuscarPorNome(string nome);
        IEnumerable<Funcionario> FiltrarFuncionario(string funcao, string nome);
    }
}
