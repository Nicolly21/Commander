namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        //Ainda falta as constrains, porém Jackson irá mostrar primeiro sem, depois ira colocar novamente
        public string HowTo { get; set; }

        public string Line { get; set; }

        public string Platform { get; set; }

    }
}