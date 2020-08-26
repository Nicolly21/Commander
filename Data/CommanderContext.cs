using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        //Criando representação do model Command(table) no BD - - No plural
        public DbSet<Command> Commands { get; set; }
    }
}