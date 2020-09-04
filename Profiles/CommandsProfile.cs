using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Map Domain Model -> DTO
            CreateMap<Command, CommandReadDto>();

            //Map DTO -> Domain Model
            CreateMap<CommandCreateDto, Command>();
            
            CreateMap<CommandUpdateDto, Command>();

            CreateMap<Command, CommandUpdateDto>();
        }
    }
}