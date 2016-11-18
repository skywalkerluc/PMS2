using SchoolManagement.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace SchoolManagement.Data.EntityConfig
{
    public class LojasRecomendadasConfig : EntityTypeConfiguration<LojasRecomendadas>
    {
        public LojasRecomendadasConfig()
        {
            HasKey(m => m.LojaId);
            Map(p =>
            {
                p.MapInheritedProperties();
                p.ToTable("LojasRecomendadas");
            });
        }
    }
}
