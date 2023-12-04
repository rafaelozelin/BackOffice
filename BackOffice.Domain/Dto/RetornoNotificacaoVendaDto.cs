using BackOffice.Domain.Enums;

namespace BackOffice.Domain.Dto
{
    public class RetornoNotificacaoVendaDto
    {
        public long CodigoPedido { get; set; }
        public string CodigoRastreio { get; set; } = default!;
        public SituacaoVenda SituacaoPedido { get; set; }
        public DateOnly PrazoEntrega { get; set; }
    }
}
