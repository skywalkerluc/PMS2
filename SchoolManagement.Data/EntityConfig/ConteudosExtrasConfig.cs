using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
