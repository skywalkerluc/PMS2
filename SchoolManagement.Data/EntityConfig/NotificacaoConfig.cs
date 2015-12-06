using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.EntityConfig
{
    public class NotificacaoConfig : EntityTypeConfiguration<Notificacao>
    {
        public NotificacaoConfig()
        {
            HasKey(p => new { p.NotificacaoId });

            Property(m => m.Assunto)
                .HasMaxLength(1000);

            Property(m => m.Descricao)
                .HasMaxLength(1000);
        }
    }
}
