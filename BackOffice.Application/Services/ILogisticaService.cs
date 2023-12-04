using BackOffice.Domain.Dto;

namespace BackOffice.Application.Services
{
    public interface ILogisticaService
    {
        public SolicitacaoTransporteLogisticaDto SolicitarEnvio(long numeroVenda);
    }
}
