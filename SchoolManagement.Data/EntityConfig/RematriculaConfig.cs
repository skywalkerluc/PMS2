using SchoolManagement.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace SchoolManagement.Data.EntityConfig
{
    public class RematriculaConfig : EntityTypeConfiguration<Rematricula>
    {
        public RematriculaConfig()
        {
            HasKey(m => m.OperacaoId);
        }
    }
}
