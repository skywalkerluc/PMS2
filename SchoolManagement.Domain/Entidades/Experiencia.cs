using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entidades
{
    public class Experiencia
    {
        
        public int ExperienciaId { get; set; }

        public string Descricao { get; set; }
        
        public string Funcao { get; set; }
        
        public DateTime AnoEntrada { get; set; }
        
        public DateTime AnoSaida { get; set; }

        public virtual Funcionario Funcionario { get; set; }
    }
}