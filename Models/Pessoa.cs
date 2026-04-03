namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models
{
    public class Pessoa
    {
        public long Id { get; set; } 

        public string Nome { get; set; } = string.Empty;

        public int Idade { get; set; }

        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

    }
}
