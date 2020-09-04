using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);       //Domain Model Command

            if(commandItem != null){
                return Ok(_mapper.Map<CommandReadDto>(commandItem));     //Swap Domain Model for DTO command
            }
            return NotFound();  //404 status code

        }

        //POST api/commands
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);  //CommandCreateDTO -> Domain Model
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();  

            //Deve retorna um CommandReadDto
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);     //Domain Model -> CommandReadDto

            //Add status 201 + Rota do GetByID
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            //Busca no Command o ID correspondente
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModelFromRepo);    //Merge current|previous 

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Tracking the changes - return Command
            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();

        }

    }
}