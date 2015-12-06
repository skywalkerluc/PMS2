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
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServico usuarioApp;

        #region Construtor

        public UsuarioController(IUsuarioServico servico)
        {
            this.usuarioApp = servico;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para inicializar a tela de login
        /// </summary>
        /// <returns>Tela Login</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método que altentica o login e recupera o indenficador do usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioViewModel usuario)
        {
            try
            {
                usuario.Cpf = "5448";
                var usuarioDominio = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                var busca = this.usuarioApp.RecuperarPorLogin(usuarioDominio);

                if (busca != null)
                {
                    Session["IdentidicadorAcessoUsuário"] = busca.indicadorAcesso;
                    Session["NomeUsuário"] = busca.Nome;
                    Session["UsuarioId"] = busca.Id;

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (InvalidOperationException e)
            {
                ViewBag.AlertMessage = "Usuário ou senha incorretos";
                return View(usuario);
            }
            //ModelState.AddModelError("", "Desculpe erro, Contate o Administrador");
            ViewBag.AlertMessage = "Desculpe erro, Contate o Administrador";
            return View(usuario);
        }

        /// <summary>
        /// Método para finalizar a sessão do usuário
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }

        /// <summary>
        /// Método para chamar a tela de criação novo usuário
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Método responsavel para criar o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(UsuarioViewModel usuario)
        {
            bool resposta = true;
            var usuarioLogin = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(this.usuarioApp.RecuperarTodos());


            if (!usuarioApp.verificarCPFSendoUtilizado(usuario.Cpf))
            {

                foreach (var item in usuarioLogin)
                {
                    if (usuario.Nome != item.Nome && usuario.UserLogin != item.UserLogin && usuario.Cpf != item.Cpf)
                    {
                        resposta = true;
                    }

                    else
                    {
                        resposta = false;
                        ViewBag.AlertMessage = "Usuário já existe!";
                        break;
                    }
                }

                if (resposta == true)
                {
                    try
                    {
                        var usuarioDominio = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                        this.usuarioApp.Incluir(usuarioDominio);
                        ViewBag.AlertMessage = "Cadastrado com Sucesso!";
                    }
                    catch (Exception ex)
                    {
                        return View(ex);
                    }
                }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");

        }

        /// <summary>
        /// Método chamar a tela alterar
        /// </summary>
        /// <returns>Tela Alterar</returns>
        public ActionResult Alterar()
        {
            var uID = Convert.ToInt32(Session["UsuarioLogadoID"]);
            //var usuarioDominio = Mapper.Map<Usuario, UsuarioViewModel>(this.usuarioApp.RecuperarPorId(uID));
            //ViewBag.EstadoUsuario = usuarioDominio.Estado;
            //ViewBag.CidadeUsuario = usuarioDominio.Cidade;

            return View();
        }

        /// <summary>
        /// Método para alterar o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Alterar(UsuarioViewModel usuario)
        {
            try
            {
                var usuarioDominio = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                //usuarioDominio.UsuarioId = Convert.ToInt32(Session["UsuarioLogadoID"]);
                //this.usuarioApp.Update(usuarioDominio);
                return RedirectToAction("Index", new { id = ViewBag.UsuarioID });
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Método responsável por deletar um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
