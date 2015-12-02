using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.EntityConfig
{
    public class FuncionarioConfig : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioConfig()
        {
            HasKey(p => new { p.Id });
            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Map(p =>
            {
                //p.MapInheritedProperties();
                p.ToTable("Funcionario");
            });

        }
    }
}
