using BackOffice.Domain.Dto;

namespace BackOffice.Application.Services
{
    public interface IVendaService
    {
        public long Criar(NotificacaoVendaDto venda);
    }
}
