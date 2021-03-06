﻿using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IAnoLetivoRepositorio : IRepositorioBase<AnoLetivo>
    {
        AnoLetivo IncluirAnoLetivo(AnoLetivo anoLetivo);
        bool RemoverAnoLetivo(int AnoLetivoId);
        bool AlterarDadosAnoLetivo(AnoLetivo anoLetivo);
    }
}
