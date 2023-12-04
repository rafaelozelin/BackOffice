using BackOffice.Application.Services;
using BackOffice.Domain.Dto;
using BackOffice.Domain.Enums;

namespace BackOffice.Application.UseCase
{
    public class NotificacaoVendaUseCase
    {
        private readonly IClienteService _clienteService;
        private readonly IVendaService _vendaService;
        private readonly IEstoqueService _estoqueService;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly ILogisticaService _logisticaService;

        public NotificacaoVendaUseCase(
            IClienteService clienteService,
            IVendaService vendaService,
            IEstoqueService estoqueService,
            INotaFiscalService notaFiscalService,
            ILogisticaService logisticaService
            )
        {
            _clienteService = clienteService;
            _vendaService = vendaService;
            _estoqueService = estoqueService;
            _notaFiscalService = notaFiscalService;
            _logisticaService = logisticaService;
        }

        public RetornoNotificacaoVendaDto Notificar(NotificacaoVendaDto notificacaoDto)
        {
            // criar/atualizar cliente
            _clienteService.CriaOuAtualiza(notificacaoDto.Cliente);
            // criar a venda no banco
            var numeroVenda = _vendaService.Criar(notificacaoDto);
            // atualizar o estoque
            _estoqueService.DecrementarEstoque(notificacaoDto.CodigoLoja, notificacaoDto.ProdutosVenda);
            // emitir a nota fiscal
            var numeroNFe = _notaFiscalService.EmitirNFe(numeroVenda);
            // solicitar a entraga
            var dadosLogistica = _logisticaService.SolicitarEnvio(numeroVenda);

            return new RetornoNotificacaoVendaDto
            {
                CodigoPedido = numeroVenda,
                CodigoRastreio = dadosLogistica.CodigoRastreio,
                PrazoEntrega = dadosLogistica.PrazoEntrega,
                SituacaoPedido = SituacaoVenda.EmSeparacao
            };
        }
    }
}
