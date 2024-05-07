

using Abrazos.Persistence.Database;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command.CreateCommand;
using ServiceEventHandler.Validators;
using ServicesQueries.Dto;
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
            //Buscar si la direccion existe en la bbdd para traer el id, sino crearlo
            ResultApp res = new ResultApp();

            using (IDbContextTransaction transac = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    _dbContext.AddRange(MapToEntity(command));
                    _dbContext.SaveChanges();
                    await transac.CommitAsync();
                    res.Succeeded = true;
                }
                catch (System.Exception ex)
                {
                    await transac.RollbackAsync();
                    string value = ((ex.InnerException != null) ? ex.InnerException!.Message : ex.Message);
                    res.message = ex.Message;
                    _logger.LogWarning(value);
                    throw;
                }
                return res;

            }

        }


        public ICollection<Event> MapToEntity(EventCreateCommand command_)
        {
            ICollection<Event> Eventos = new List<Event>();

            foreach(var e in command_.DateTimes)
            {
                Event entity = new Event();
                entity.UserIdCreator = command_.UserIdCreator;
                entity.Name = command_.Name;
                entity.Description = command_.Description;
                entity.Image = command_.Image ?? null;
                entity.DateInit = e.DateInit;
                entity.DateFinish = e.DateFinish;
                entity.EventStateId = command_.EventStateId;
                entity.TypeEventId = command_.TypeEventId;
                entity.LevelId = command_.LevelId != null ? command_.LevelId : null;
                entity.RolId = command_.RolId != null ? command_.LevelId : null;
                entity.Couple = command_.Couple;
                entity.Cupo = command_.Cupo;

                if (!Validations.IdOrObjectMandatory(command_.AddressId, command_.Address))
                {
                    throw new Exception("La direccion o el identificador de la misma son necesarios.");
                }

                entity.AddressId = command_.AddressId ?? 0;
                if (command_.Address != null)
                {
                    Address Address_ = new Address();
                    Address_.Street = command_.Address.Street;
                    Address_.Number = command_.Address.Number;
                    Address_.UserId = command_.UserIdCreator;
                    Address_.StateAddress = true;
                    Address_.DetailAddress = command_.Address.DetailAddress;
                    Address_.VenueName = command_.Address.VenueName?? null;

                    // If City exist use the id, else create a new city and verify if country exist -
                    City cityentity = GetCity(command_.Address.CityName);
                    if(cityentity != null)
                    {
                        Address_.CityId= cityentity.CityId;
                    }
                    else
                    {
                        City city = new City();                      
                        Country countryEntity = GetCountrie(command_.Address.CityName);
                        if (countryEntity != null)
                            city.CountryId = countryEntity.CountryId;
                        else
                        {
                            countryEntity =  new Country();
                            countryEntity.Name = command_.Address.CountryName;
                            city.Country = countryEntity;
                        }
                        Address_.City = city;
                        
                    }                
                    entity.Address = Address_;

                }
                

                entity.CycleId = command_.CycleId ?? 0;
                if (command_.CycleId == null || command_.CycleId == 0)
                {
                    Cycle cicle = new Cycle();
                    cicle.CycleTitle = command_.Name;
                    cicle.Description = command_.Description;
                    entity.Cycle = cicle;
                }

                Eventos.Add(entity);
            }

            return Eventos;
        }

        public  City GetCity(string name)
        {
            var entity =  _dbContext.Cities
                              .SingleOrDefault(x => x.CityName.Equals(name));

            _logger.LogInformation(entity.ToString());

            return entity;
        }
        public Country GetCountrie(string name)
        {
            var entity = _dbContext.Conutry
                              .SingleOrDefault(x => x.Name.Equals(name));

            _logger.LogInformation(entity.ToString());

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
        public  Event MapToEntityUpdate(EventUpdateCommand command_)
        {
            var entity =  _dbContext.Event
                                     .SingleOrDefault(x => x.EventId == command_.EventId);

            entity.EventId=command_.EventId;
            entity.UserIdCreator = command_.UserIdCreator??entity.UserIdCreator;
            entity.Name = command_.Name?? entity.Name;
            entity.Description = command_.Description?? entity.Description;
            entity.DateInit = command_.DateInit?? entity.DateInit;
            entity.DateFinish = command_.DateFinish ?? entity.DateFinish;
            entity.EventStateId = command_.EventStateId ?? entity.EventStateId;
            entity.TypeEventId = command_.TypeEventId ?? entity.TypeEventId;
            entity.LevelId = command_.LevelId ?? entity.LevelId;
            entity.RolId = command_.RolId ?? entity.RolId ;
            entity.Couple = command_.Couple ?? entity.Couple;
            entity.Cupo = command_.Cupo ?? entity.Cupo;
            entity.AddressId = command_.AddressId ?? entity.AddressId;
            entity.CycleId = command_.CycleId ?? entity.CycleId;


            return entity;
        }
    }
}

