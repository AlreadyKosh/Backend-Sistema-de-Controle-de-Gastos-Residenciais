using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Categorias
{
    public class CategoriaResponseDto
    {
        [Required]
        [MaxLength(200)]
        [DefaultValue("Fernando")]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [MaxLength(400)]
        [DefaultValue("Conta de Luz")]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [DefaultValue(FinalidadeCategoria.Despesa)]
        public FinalidadeCategoria Finalidade { get; set; }
    }
}
