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
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Nome)
                .IsRequired();

            Property(m => m.UserLogin)
              .IsRequired()
              .HasMaxLength(50);

            Property(m => m.Senha)
              .IsRequired()
              .HasMaxLength(20);

            Property(m => m.Endereco.Numero)
                .IsOptional();

            Property(m => m.Foto)
                .IsOptional();

            

        }
    }
}
