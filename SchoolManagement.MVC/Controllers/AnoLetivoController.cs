using AutoMapper;
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
            var anos = _anoLetivoServico.RecuperarTodos();
            var anosMapped = Mapper.Map<IEnumerable<AnoLetivo>, IEnumerable<AnoLetivoViewModel>>(anos);
            return View("VisualizarAnosLetivos", anosMapped);
        }

        //
        // GET: /AnoLetivo/Details/5
        public ActionResult Details(int id)
        {
            var anos = _anoLetivoServico.Recuperar(id);
            var anoMapped = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anos);
            return View("VisualizarDadosAnoLetivo", anoMapped);
        }

        //
        // GET: /AnoLetivo/Create
        public ActionResult Create()
        {
            var anoLetivo = new AnoLetivoViewModel();
            return View("IncluirAnoLetivo", anoLetivo);
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

                    throw new NotImplementedException("Erro ao adicionar novo ano letivo");
                }

                var anoLetivoMapped = Mapper.Map<AnoLetivoViewModel, AnoLetivo>(anoLetivo);
                var attempt = _anoLetivoServico.IncluirAnoLetivo(anoLetivoMapped);

                if (attempt != null)
                {
                    return View("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao adicionar novo ano letivo";
                    throw new NotImplementedException("Erro ao adicionar novo ano letivo");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao adicionar novo ano letivo";
                throw new NotImplementedException("Erro ao adicionar novo ano letivo");
            }
        }

        //
        // GET: /AnoLetivo/Edit/5
        public ActionResult Edit(int id)
        {
            var anos = _anoLetivoServico.Recuperar(id);
            var anoMapped = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anos);
            return View("AtualizarDadosAnoLetivo", anoMapped);
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
                    return View("Index", "Home");
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
            return View("RemoverAnoLetivo", anoMapped);
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
                    return View("Index", "Home");
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
    }
}
