namespace BackOffice.Domain.Dto
{
    public class ClienteNotificacaoVendaDto
    {
        public string Documento { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Telefone { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
