namespace BackOffice.Domain.Dto
{
    public class SolicitacaoTransporteLogisticaDto
    {
        public string CodigoRastreio { get; set; } = default!;
        public DateOnly PrazoEntrega { get; set; }
    }
}
