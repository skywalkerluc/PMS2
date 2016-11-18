using SchoolManagement.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace SchoolManagement.Data.EntityConfig
{
    public class ResponsavelConfig : EntityTypeConfiguration<Responsavel>
    {
        public ResponsavelConfig()
        {
            //HasKey(e => e.Id);

            Map(e =>
                {
                    //e.MapInheritedProperties();
                    e.ToTable("Responsavel");
                });
        }
    }
}
