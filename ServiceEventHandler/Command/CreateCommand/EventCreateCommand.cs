using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class EventCreateCommand
    {
        [Required]// data anotation not equal zero agregar!!

        public int UserIdCreator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AddressId { get; set; }//validar uno u otro
        public string? Image { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateFinish { get; set; }
        public int EventStateId { get; set; }
        public int TypeEventId{ get; set; }

        public int RolId { get; set; }
        public int LevelId { get; set; }
        public int Cupo { get; set; }
        public bool Couple { get; set; }
        public int? CycleId { get; set; }

        public int eventState { get; set; }
        public AddressCreateCommand? Address { get; set; }
        public CycleCommandCreate? Cycle { get; set; }




    }
}