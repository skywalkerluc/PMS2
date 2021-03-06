﻿using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class LojasRecomendadasServico : ServicoBase<LojasRecomendadas>, ILojasRecomendadasServico
    {
        private readonly ILojasRecomendadasRepositorio _lojasRecRep;

        public LojasRecomendadasServico(ILojasRecomendadasRepositorio lojasRecRep)
            :base(lojasRecRep)
        {
            _lojasRecRep = lojasRecRep;
        }
    }
}
