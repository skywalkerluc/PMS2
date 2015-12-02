using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class FuncionarioServico : ServicoBase<Funcionario>, IFuncionarioServico
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioServico(IFuncionarioRepositorio funcionarioRepositorio)
            :base(funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public IEnumerable<Funcionario> BuscarPorNome(string nome)
        {
            return this._funcionarioRepositorio.BuscarPorNome(nome);
        }

        public IEnumerable<Funcionario> FiltrarFuncionario(string funcao, string nome)
        {
            return this._funcionarioRepositorio.FiltrarFuncionario(funcao, nome);
        }

    }
}
