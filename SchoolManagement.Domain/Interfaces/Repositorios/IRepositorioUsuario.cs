﻿using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        //Metodo para recuperar o login do banco. Metodo Implementado no UsuarioRepositorio.
        Usuario RecuperarPorLogin(Usuario usuario);
        bool verificarCPFSendoUtilizado(string cpf);
        bool VerificarLoginExistente(string login);
        bool VerificarRGSendoUtilizado(string rg);
        IEnumerable<Notificacao> ExibirNotificacoesUsuario(int indicadorAcesso, int idUsuario);
        //bool VerificarLoginExistente(string login);
    }
}
