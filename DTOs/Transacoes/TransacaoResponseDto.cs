namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Transacoes
{
    public class TransacaoResponseDto
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public string NomePessoa { get; set; }
        public string Categoria { get; set; }
    }
}
