using SchoolManagement.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace SchoolManagement.Data.EntityConfig
{
    public class ConteudosExtrasConfig : EntityTypeConfiguration <ConteudosExtras>
    {
        public ConteudosExtrasConfig()
        {
            HasKey(m => m.ConteudoId);
        }
    }
}
