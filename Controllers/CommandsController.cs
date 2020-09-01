using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : Controller
    {

        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        //Construtor para que essa depenência seja injetada
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //Get api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));

        }

        //Get api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);       //Domain Model Command

            if(commandItem != null){
                return Ok(_mapper.Map<CommandReadDto>(commandItem));     //Swap Domain Model for DTO command
            }
            return NotFound();  //404 status code

        }
    }
}