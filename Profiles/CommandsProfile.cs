using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Mapeamento entre Domain Model e DTO
            CreateMap<Command, CommandReadDto>();

            //Mapeamento entre DTO e Domain Model
            CreateMap<CommandCreateDto, Command>();
        }
    }
}