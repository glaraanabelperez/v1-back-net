

using Abrazos.Persistence.Database;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command.CreateCommand;
using ServiceEventHandler.Validators;
using Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            try
            {
                var resEntity = await this.commandGeneric.Add<Event>(MapToEntity(command));
                res.Succeeded = true;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
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

            if (!IdOrObjectMandatory.Validate(command_.AddressId, command_.Address))
            {
                throw new Exception("La direccion no puede estar vacia");
            }

            entity.AddressId = command_.AddressId ?? 0;
            if(command_.Address != null )
            {
               Address Address_ = new Address();
               Address_.Street = command_.Address.Street;
               Address_.Number = command_.Address.Number;
               //If the direction is new, only in case of creating an event,  the user id is the creator.
               Address_.UserId = command_.UserIdCreator;
               Address_.StateAddress = command_.Address.StateAddress;
               Address_.DetailAddress = command_.Address.DetailAddress;

               if (command_.Address.CityId == null && string.IsNullOrEmpty(command_.Address.city.CityName))
               {
                   throw new Exception("La ciudad no puede estar vacia");
               }

               Address_.CityId = command_.Address.CityId ?? 0;
               if(command_.Address.CityId == null)
               {
                  City city = new City();
                  city.CityName = command_.Address.city.CityName;
                  city.StateName = command_.Address.city.StateName;

                  //Validating and adding Country
                  if (command_.Address.city.CountryId == null || string.IsNullOrEmpty(command_.Address.city.CountryName))
                  {
                      throw new Exception("El pais no puede estar vacio");
                  }
                  city.CountryId = command_.Address.city.CountryId ?? 0;
                  if (command_.Address.city.CountryId == null)
                  {
                      city.Country = new Country()
                      {
                          Name = command_.Address.city.CountryName
                      };
                  }    
                  //Adding city
                  Address_.City = city;
                }
                
               entity.Address = Address_;

            }
            else
            {
                throw new Exception("La direccion no puede estar vacia");
            }

            entity.CycleId = command_.CycleId ?? 0;
            if (command_.CycleId == null)
            {
                Cycle cicle = new Cycle();
                cicle.CycleTitle = command_.Cycle.Tittle;
                cicle.Description= command_.Cycle.Description;
                entity.Cycle = cicle;
            }

            return entity;
        }

        public async Task<ResultApp> Update(EventUpdateCommand command_)
        {
            ResultApp res = new ResultApp();
            try
            {
                var resEntity = await this.commandGeneric.Update<Event>(MapToEntityUpdate(command_));
                res.Succeeded = true;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }
        public Event MapToEntityUpdate(EventUpdateCommand command_)
        {
            Event entity = new Event();
            entity.UserIdCreator = command_.UserIdCreator;
            entity.Name = command_.Name;
            entity.Description = command_.Description;
            entity.DateInit = command_.DateInit;
            entity.DateFinish = command_.DateFinish;
            entity.EventStateId = command_.EventStateId;
            entity.TypeEventId = command_.TypeEventId;
            entity.LevelId = command_.LevelId != null ? command_.LevelId : null;
            entity.RolId = command_.RolId != null ? command_.LevelId : null; ;
            entity.Couple = command_.Couple;
            entity.Cupo = command_.Cupo;
            entity.AddressId = command_.AddressId;
            entity.CycleId = command_.CycleId;


            return entity;
        }
    }
}

