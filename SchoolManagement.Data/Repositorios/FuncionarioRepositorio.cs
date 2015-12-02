using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class FuncionarioRepositorio : RepositorioBase<Funcionario>, IFuncionarioRepositorio
    {
        public IEnumerable<Funcionario> BuscarPorNome(string nome)
        {
            return Db.Funcionarios.Where(p => p.Nome.Contains(nome));
        }

        public IEnumerable<Funcionario> FiltrarFuncionario(string funcao, string nome)
        {
            var funcionarioFiltro = from d in Db.Funcionarios
                                   where d.Funcao == funcao || funcao == string.Empty || funcao == null
                                   where d.Nome.Contains(nome) || nome == string.Empty || nome == null
                                   select d;

            return funcionarioFiltro;
        }

    }
}
