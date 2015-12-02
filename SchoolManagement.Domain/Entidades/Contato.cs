using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Contato
    {
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

    }
}
