using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entidades
{
    
    public class Usuario
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public byte Foto { get; set; }
        public Sexo Sexo { get; set; }
        

        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }

        public string UserLogin { get; set; }
        public string Senha { get; set; }

        public int indicadorAcesso { get; set; }

    }
}
