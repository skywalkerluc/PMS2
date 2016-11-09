using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.Controllers.NewControllers
{
    public class Util
    {
        public DateTime TratarData(int dia, int mes, int ano)
        {
            DateTime date = Convert.ToDateTime(dia + "/" + mes + "/" + ano);
            return (Convert.ToDateTime(date.ToString("dd/MM/yyyy")));
        }
    }
}