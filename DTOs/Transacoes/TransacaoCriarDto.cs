using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Transacoes
{
    public class TransacaoCriarDto
    {
        [Required]
        [MaxLength(400)]
        [DefaultValue("Conta de Luz")]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        [DefaultValue(250.50)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Valor { get; set; }
        [Required]
        [DefaultValue(TipoTransacao.Despesa)]
        public TipoTransacao Tipo { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "PessoaId deve ser maior que 0")]
        public long PessoaId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "CategoriaId deve ser maior que 0")]
        public long CategoriaId { get; set; }
    }
}
