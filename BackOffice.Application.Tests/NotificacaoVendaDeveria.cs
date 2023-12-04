using BackOffice.Application.Services;
using BackOffice.Application.UseCase;
using BackOffice.Domain.Dto;
using BackOffice.Domain.Enums;
using FluentAssertions;
using Moq;

namespace BackOffice.Application.Tests
{
    public class NotificacaoVendaDeveria
    {
        [Fact]
        public void RetornarSucessoQuandoVendaValida()
        {
            long vendaId = 1;

            //Arrange
            var clienteService = new Mock<IClienteService>(MockBehavior.Strict);
            clienteService.Setup(service => service.CriaOuAtualiza(It.IsAny<ClienteNotificacaoVendaDto>()));

            var vendaService = new Mock<IVendaService>(MockBehavior.Strict);
            vendaService.Setup(service => service.Criar(It.IsAny<NotificacaoVendaDto>()))
                .Returns(vendaId);

            var estoqueService = new Mock<IEstoqueService>(MockBehavior.Strict);
            estoqueService.Setup(service => service.DecrementarEstoque(It.IsAny<long>(), It.IsAny<IEnumerable<ProdutoNotificacaoVendaDto>>()))
                .Returns(1);

            var notaFiscalService = new Mock<INotaFiscalService>(MockBehavior.Strict);
            notaFiscalService.Setup(service => service.EmitirNFe(vendaId))
                .Returns("nfe34543");

            var logisticaService = new Mock<ILogisticaService>(MockBehavior.Strict);
            logisticaService.Setup(service => service.SolicitarEnvio(vendaId))
                .Returns(new SolicitacaoTransporteLogisticaDto()
            {
                CodigoRastreio = "BR34334",
                PrazoEntrega = DateOnly.FromDateTime(DateTime.Now.AddDays(3))
            });

            var casoDeUso = new NotificacaoVendaUseCase(clienteService.Object,
                vendaService.Object,
                estoqueService.Object,
                notaFiscalService.Object,
                logisticaService.Object
                );

            var notificacaoDto = new NotificacaoVendaDto();

            //Act
            RetornoNotificacaoVendaDto retorno = casoDeUso.Notificar(notificacaoDto);

            //Assert
            var hoje = DateOnly.FromDateTime(DateTime.Today);

            retorno.CodigoPedido
                .Should()
                .BeGreaterThan(0);

            retorno.CodigoRastreio
                .Should()
                .NotBeNullOrEmpty();

            retorno.SituacaoPedido
                .Should()
                .Be(SituacaoVenda.EmSeparacao);

            retorno.PrazoEntrega
                .Should()
                .BeOnOrAfter(hoje);

            vendaService.Verify(service => service.Criar(It.IsAny<NotificacaoVendaDto>()), Times.Once);

            estoqueService.Verify(service => service.DecrementarEstoque(It.IsAny<long>(), It.IsAny<IEnumerable<ProdutoNotificacaoVendaDto>>()), Times.Once);

            notaFiscalService.Verify(service => service.EmitirNFe(vendaId), Times.Once);

            logisticaService.Verify(service => service.SolicitarEnvio(vendaId), Times.Once);
        }
    }
}