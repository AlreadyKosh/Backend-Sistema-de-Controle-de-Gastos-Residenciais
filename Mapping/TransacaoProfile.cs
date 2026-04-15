using AutoMapper;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Categorias;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Transacoes;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Mapping
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            CreateMap<Transacao, TransacaoResponseDto>();
            CreateMap<TransacaoResponseDto, Transacao>();

            CreateMap<Transacao, TransacaoResponseDto>()
                .ForMember(dest => dest.NomePessoa,
                    opt => opt.MapFrom(src => src.Pessoa.Nome))
                .ForMember(dest => dest.Categoria,
                    opt => opt.MapFrom(src => src.Categoria.Descricao));
        }
    }
}
