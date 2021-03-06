﻿using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class AnoLetivoController : Controller
    {
        private readonly IAnoLetivoServico _anoLetivoServico;

        public AnoLetivoController(IAnoLetivoServico anoLetivoServico)
        {
            _anoLetivoServico = anoLetivoServico;
        }

        //
        // GET: /AnoLetivo/
        public ActionResult Index()
        {
            //var anos = _anoLetivoServico.RecuperarTodos();
            //var anosMapped = Mapper.Map<IEnumerable<AnoLetivo>, IEnumerable<AnoLetivoViewModel>>(anos);
            //return View("VisualizarAnosLetivos", anosMapped);
            var disc = _anoLetivoServico.RecuperarTodos();
            var discMapped = Mapper.Map<IEnumerable<AnoLetivo>, IEnumerable<AnoLetivoViewModel>>(disc);
            return View("VisualizarAnoLetivo", discMapped);
        }

        //
        // GET: /AnoLetivo/Details/5
        public ActionResult Details(int id)
        {
            var anos = _anoLetivoServico.Recuperar(id);
            var anoMapped = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anos);
            return View("DetalhesAnoLetivo", anoMapped);
        }

        //
        // GET: /AnoLetivo/Create
        public ActionResult Create(string mensageAlert)
        {
            ViewBag.AlertMessage = mensageAlert;
            var anoLetivo = new AnoLetivoViewModel();
            return View("CreateAnoLetivo", anoLetivo);
        }

        //
        // POST: /AnoLetivo/Create
        [HttpPost]
        public ActionResult Create(AnoLetivoViewModel anoLetivo)
        {
            try
            {
                if (anoLetivo.QntUnidades < 1)
                {

                    ViewBag.AlertMessage = "Erro ao adicionar novo ano letivo";
                    var mensageAlert = ViewBag.AlertMessage;
                    return RedirectToAction("Create", "AnoLetivo", new { mensageAlert });
                }

                var anoLetivoMapped = Mapper.Map<AnoLetivoViewModel, AnoLetivo>(anoLetivo);
                var attempt = _anoLetivoServico.IncluirAnoLetivo(anoLetivoMapped);

                if (attempt != null)
                {
                    ViewBag.AlertMessage = "Ano letivo cadastrado com sucesso.";
                    var mensageAlert = ViewBag.AlertMessage;
                    return RedirectToAction("Index", "Home", new { mensageAlert });
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao adicionar novo ano letivo";
                    var mensageAlert = ViewBag.AlertMessage;
                    return RedirectToAction("Create", "AnoLetivo", new { mensageAlert });
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao adicionar novo ano letivo";
                var mensageAlert = ViewBag.AlertMessage;
                return RedirectToAction("Create", "AnoLetivo", new { mensageAlert });
            }
        }

        //
        // GET: /AnoLetivo/Edit/5
        public ActionResult Edit(int id)
        {
            var anos = _anoLetivoServico.Recuperar(id);
            var anoMapped = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anos);
            return View("EditarAnoLetivo", anoMapped);
        }

        //
        // POST: /AnoLetivo/Edit/5
        [HttpPost]
        public ActionResult Edit(AnoLetivoViewModel anoLetivo)
        {
            try
            {
                if (anoLetivo.QntUnidades < 1)
                {
                    ViewBag.AlertMessage = "Erro ao atualizar dados de determinado ano letivo";
                    throw new NotImplementedException("Erro ao atualizar dados de determinado ano letivo");
                }

                var anoLetivoMapped = Mapper.Map<AnoLetivoViewModel, AnoLetivo>(anoLetivo);
                var attempt = _anoLetivoServico.AlterarDadosAnoLetivo(anoLetivoMapped);

                if (attempt)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao atualizar dados de determinado ano letivo";
                    throw new NotImplementedException("Erro ao atualizar dados de determinado ano letivo");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao atualizar dados de determinado ano letivo";
                throw new NotImplementedException("Erro ao atualizar dados de determinado ano letivo");
            }
        }

        //
        // GET: /AnoLetivo/Delete/5
        public ActionResult Delete(int id)
        {
            var anos = _anoLetivoServico.Recuperar(id);
            var anoMapped = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anos);
            return View("DeletarAnoLetivo", anoMapped);
        }

        //
        // POST: /AnoLetivo/Delete/5
        [HttpPost]
        public ActionResult Delete(AnoLetivoViewModel anoLetivo)
        {
            try
            {
                var anoLetivoMapped = Mapper.Map<AnoLetivoViewModel, AnoLetivo>(anoLetivo);
                var attempt = _anoLetivoServico.RemoverAnoLetivo(anoLetivoMapped.AnoLetivoId);

                if (attempt != false)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao remover determinado ano letivo";
                    throw new NotImplementedException("Erro ao remover determinado ano letivo");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao remover determinado ano letivo";
                throw new NotImplementedException("Erro ao remover determinado ano letivo");
            }
        }

        public ActionResult VisualizarAnosLetivo()
        {
            var disc = _anoLetivoServico.RecuperarTodos();
            var discMapped = Mapper.Map<IEnumerable<AnoLetivo>, IEnumerable<AnoLetivoViewModel>>(disc);
            return View("VisualizarAnoLetivo", discMapped);
        }
    }
}
