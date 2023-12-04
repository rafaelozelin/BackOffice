using BackOffice.Domain.Enums;

namespace BackOffice.Domain.Dto
{
    public class NotificacaoVendaDto
    {
        public string CodigoVendaEcommerce { get; set; } = default!;
        public decimal ValorVenda { get; set; }
        public MeioPagamento MeioPagamento { get; set; }
        public ClienteNotificacaoVendaDto Cliente { get; set; } = default!;
        public EnderecoNotificacaoVendaDto EnderecoEntrega { get; set; } = default!;
        public IEnumerable<ProdutoNotificacaoVendaDto> ProdutosVenda { get; set; } = default!;
        public long CodigoLoja { get; set; }
        public long NumeroVendedor { get; set; }
    }
}
