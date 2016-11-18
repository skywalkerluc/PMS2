using System.Collections.Generic;

namespace SchoolManagement.Domain.Cross
{
    public class RetornoBase
    {
        public bool Status { get; set; }
        public int RegistroRetorno { get; set; }
        public List<CamposOutput> ListaOutput { get; set; }
        public string Mensagem { get; set; }

        public RetornoBase()
        {
            ListaOutput = new List<CamposOutput>();
        }

    }

    public class RetornoBase<T> : RetornoBase
    {
        public List<T> ListaDados { get; set; }
        public T Dados { get; set; }

        public RetornoBase()
        {
            ListaDados = new List<T>();
            ListaOutput = new List<CamposOutput>();
        }
    }

    public class CamposOutput
    {
        public string Nome { get; set; }
        public string Valor { get; set; }
    }
}
