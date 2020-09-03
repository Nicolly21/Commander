using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetAllCommands();  //lista do DB Internal
        Command GetCommandById(int id);

        //CREATE
        void CreateCommand(Command cmd);

        //UPDATE
        void UpdateCommand(Command cmd);
    } 
}