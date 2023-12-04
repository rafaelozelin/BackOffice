namespace BackOffice.Domain.Dto
{
    public class EnderecoNotificacaoVendaDto
    {
        public string Cep { get; set; } = default!;
        public string Logradouro { get; set; } = default!;
        public string Numero { get; set; } = default!;
        public string Bairro { get; set; } = default!;
        public int CidadeIBGE { get; set; }
        public string UF { get; set; } = default!;
        public string Complemento { get; set; } = default!;
    }
}
