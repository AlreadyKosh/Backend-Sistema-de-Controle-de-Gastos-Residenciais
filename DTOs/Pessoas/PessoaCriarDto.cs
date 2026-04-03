using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Pessoas
{
    public class PessoaCriarDto
    {
        [Required]
        [MaxLength(200)]
        [DefaultValue("Fernando")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(0, 150)]
        [DefaultValue(10)]
        public int Idade { get; set; }
    }
}
