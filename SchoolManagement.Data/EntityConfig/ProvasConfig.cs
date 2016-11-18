using SchoolManagement.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

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
