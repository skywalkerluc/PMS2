using SchoolManagement.Domain.Entidades;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IAnoLetivoServico : IServicoBase<AnoLetivo>
    {
        AnoLetivo IncluirAnoLetivo(AnoLetivo anoLetivo);
        bool RemoverAnoLetivo(int AnoLetivoId);
        bool AlterarDadosAnoLetivo(AnoLetivo anoLetivo);
    }
}
