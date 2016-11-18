using SchoolManagement.Domain.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SchoolManagement.Data.EntityConfig
{
    public class ProfessorConfig : EntityTypeConfiguration<Professor>
    {
        public ProfessorConfig()
        {
            //HasKey(p => new { p.Id });
            HasKey(p => new { p.Id });
            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Map(p =>
                {
                    //p.MapInheritedProperties();
                    p.ToTable("Professor");
                });
        }
    }
}
