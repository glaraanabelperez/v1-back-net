

using Abrazos.Persistence.Database;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command.CreateCommand;
using ServiceEventHandler.Validators;
using Utils;

namespace Abrazos.ServiceEventHandler
{
    public class EventCommandService: IEventCommandService
    {
        private readonly ILogger<EventCommandService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public IGenericRepository commandGeneric;

        public EventCommandService(ApplicationDbContext dbContext, IGenericRepository command, 
            ILogger<EventCommandService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            this.commandGeneric = command;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultApp> Add(EventCreateCommand command)
        {
            ResultApp res = new ResultApp();

            if (IdOrObjectMandatory.Validate(command.AddressId, command.Address))
            {
                try
                {
                    var resEntity = await this.commandGeneric.Add<Event>(MapToEntity(command));
                    res.Succeeded = true;
                }
                catch (Exception ex)
                {
                    res.message = ex.Message;
                }
            }
            return res;

        }


        public Event MapToEntity(EventCreateCommand command_)
        {
            Event entity = new Event();
            entity.UserIdCreator = command_.UserIdCreator;
            entity.Name = command_.Name;
            entity.Description= command_.Description;
            entity.Image = command_.Image ?? null;
            entity.DateInit = command_.DateInit;
            entity.DateFinish = command_.DateFinish;
            entity.EventStateId = command_.EventStateId;
            entity.TypeEventId = command_.TypeEventId;
            entity.LevelId = command_.LevelId != null ? command_.LevelId  : null ;
            entity.RolId = command_.RolId != null ? command_.LevelId : null; ;
            entity.Couple = command_.Couple;
            entity.Cupo = command_.Cupo;

            if ( command_.AddressId != null)
            {
                entity.AddressId = command_.AddressId ?? 0;          
            }
            else if(command_.Address != null )
            {
                Address Address_ = new Address();
                Address_.Street = command_.Address.Street;
                Address_.Number = command_.Address.Number;
                Address_.UserId = command_.Address.UserId;
                Address_.StateAddress = command_.Address.StateAddress;
                Address_.DetailAddress = command_.Address.DetailAddress;
                Address_.CityId = command_.Address.CityId ?? 0;

                if (command_.Address.CityId == null)
                {
                    City city = new City();
                    city.CityName = command_.Address.city.CityName;
                    city.CountryName = command_.Address.city.CountryName;
                    city.StateName = command_.Address.city.StateName;
                    Address_.City = city;
                }
                
                entity.Address = Address_;

            }
            else
            {
                throw new Exception("La direccion no puede estar vacia");
            }
            if(command_.CycleId!= null)
            {
                entity.CycleId = command_.CycleId;

            }else if (command_.Cycle != null)
            {
                Cycle cicle = new Cycle();
                cicle.CycleTitle = command_.Cycle.Tittle;
                cicle.Description= command_.Cycle.Description;
                entity.Cycle = cicle;
            }

            return entity;
        }

        public Task<ResultApp> Update(EventUpdateCommand entity)
        {
            throw new NotImplementedException();
        }
    }
}

