namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models
{
    public class Categoria
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public FinalidadeCategoria Finalidade { get; set; }
    }
}
