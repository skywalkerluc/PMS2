using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Contrato
    {
        public int ContratoId { get; set; }
        public DateTime DataInicioContrato { get; set; }
        public decimal ValorDesconto { get; set; }
        public DateTime DataValidadeMensal { get; set; }
        public DateTime DataPgtoMensalidade { get; set; }
        public decimal ValorMensalidade { get; set; }
        public Aluno Aluno { get; set; }
        
    }
}
