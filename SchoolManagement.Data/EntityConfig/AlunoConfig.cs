using SchoolManagement.Domain.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SchoolManagement.Data.EntityConfig
{
    public class AlunoConfig : EntityTypeConfiguration<Aluno>
    {
        public AlunoConfig()
        {
            HasKey(p => new { p.Id });
            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Map(p =>
                {
                    //p.MapInheritedProperties();
                    p.ToTable("Aluno");
                });


        }
    }
}
