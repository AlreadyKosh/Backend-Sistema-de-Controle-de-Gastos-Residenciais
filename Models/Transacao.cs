namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models
{
    public class Transacao
    {
        public long Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public TipoTransacao Tipo { get; set; }

        public long PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;

        public long CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

    }
}
