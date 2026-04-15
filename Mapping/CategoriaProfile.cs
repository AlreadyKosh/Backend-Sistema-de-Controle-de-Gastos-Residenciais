using AutoMapper;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Categorias;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Pessoas;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Mapping
{
    public class CategoriaProfile:Profile   
    {
        public CategoriaProfile()
        {
            CreateMap<CategoriaResponseDto, Categoria>();
            CreateMap<Categoria, CategoriaResponseDto>();

            CreateMap<CategoriaResponseDto, Categoria>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
