using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Número de caracteres não permitido.")]
        [MinLength(2, ErrorMessage = "Número de caracteres não permitido.")]
        [Display(Name = "Nome completo")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [MaxLength(9, ErrorMessage = "Número de caracteres não permitido.")]
        [Display(Name = "RG")]
        public string Rg { get; set; }

        [MaxLength(14, ErrorMessage = "Número de caracteres não permitido.")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        public string Nacionalidade { get; set; }

        public string Naturalidade { get; set; }

        //public byte Foto { get; set; }

        public Sexo Sexo { get; set; }
        
        public EnderecoViewModel Endereco { get; set; }

        public ContatoViewModel Contato { get; set; }

        #region Informações Login

        [Required(ErrorMessage = "Login é necessário para prosseguir.")]
        [MaxLength(50, ErrorMessage = "Número de caracteres não permitido.")]
        [MinLength(4, ErrorMessage = "Número de caracteres não permitido.")]
        [Display(Name = "Nome de usuário")]
        public string UserLogin { get; set; }
        
        [Required(ErrorMessage = "Uma senha é necessária para prosseguir.")]
        [MaxLength(20, ErrorMessage = "Número de caracteres não permitido.")]
        [MinLength(4, ErrorMessage = "Número de caracteres não permitido.")]
        public string Senha { get; set; }
        
        [Display(Name = "Confirmar senha")]
        public string ConfirmarSenha { get; set; }

        [ScaffoldColumn(false)]
        public int indicadorAcesso { get; set; }

        #endregion


        
    }
}