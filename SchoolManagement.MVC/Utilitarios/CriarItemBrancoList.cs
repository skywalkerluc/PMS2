using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.Utilitarios
{
    public class CriarItemBrancoList
    {
        public void CriarItemBrancoEmLista(List<SelectListItem> ListaSelecionaveis)
        {
            SelectListItem selectList = new SelectListItem()
            {
                Text = string.Empty,
                Value = string.Empty,
                Selected = true
            };
            ListaSelecionaveis.Add(selectList);
        }

        
    }
}