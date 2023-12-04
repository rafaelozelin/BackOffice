using BackOffice.Domain.Dto;

namespace BackOffice.Application.Services
{
    public interface IEstoqueService
    {
        public long DecrementarEstoque(long codigoLoja, IEnumerable<ProdutoNotificacaoVendaDto> produtos);
    }
}
