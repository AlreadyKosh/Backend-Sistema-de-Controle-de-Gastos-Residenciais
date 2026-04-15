using AutoMapper;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Pessoas;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Mapping
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaResponseDto, Pessoa>();
            CreateMap<Pessoa, PessoaResponseDto>();

            CreateMap<PessoaResponseDto, Pessoa>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Transacoes, opt => opt.Ignore());
        }
    }
}
