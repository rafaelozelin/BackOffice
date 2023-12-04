using BackOffice.Domain.Dto;

namespace BackOffice.Application.Services
{
    public interface IClienteService
    {
        public void CriaOuAtualiza(ClienteNotificacaoVendaDto cliente);
    }
}
