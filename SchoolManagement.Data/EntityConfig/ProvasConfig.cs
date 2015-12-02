using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.EntityConfig
{
    public class ProvasConfig : EntityTypeConfiguration<Prova>
    {
        public ProvasConfig()
        {
            HasKey(m => m.ProvaId);
        }
    }

    public class ResultadosProvasConfig : EntityTypeConfiguration<ResultadosProvas>
    {
        public ResultadosProvasConfig()
        {
            HasKey(m => new { m.ResultadoId });
        }
    }
}
