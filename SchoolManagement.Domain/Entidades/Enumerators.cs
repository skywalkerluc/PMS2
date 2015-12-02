using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public enum Sexo
    {
        Masculino,
        Feminino
    }

    public enum Etnia
    {
        Branco,
        Negro,
        Pardo,
        Mulato,
        Índio,
        Caboclo,
        Cafuzo
    }

    public enum StatusProva
    {
        Pendente =  1,
        Concluída = 2
    }

    public enum StatusCadastro
    {
        Cadastrado   
    }

    public enum TipoProva
    {
        ProvaNormal,
        Recuperação,
        SegundaChamada,
        RecuperaçãoFinal
    }

    public enum StatusPagamento
    {

    }

    public enum TipoContato
    {

    }

    //public enum IndicadorAcesso
    //{
    //    Usuario = 1,
    //    Aluno = 2,
    //    Funcionario = 3,
    //    Professor = 4,
    //    Responsavel = 5
    //    Administrador = 6
    //}


    public enum HorariosTurma
    {
        Manhã = 1, 
        Tarde = 2,
        Noite = 3
    }
}
