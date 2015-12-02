
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
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
        Pendente = 1,
        Concluída = 2
    }

    public enum StatusCadastro
    {

    }

    public enum TipoProva
    {
        ProvaNormal = 1,
        ProvaDeRecuperacao1 = 2,
        ProvaDeRecuperacao2 = 3,
        ProvaDeSegundaChamada = 4,
        ProvaFinal = 5
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
    //      Adm = 6
    //}
}

