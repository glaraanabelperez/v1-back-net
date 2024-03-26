

using Abrazos.Persistence.Database;
using Abrazos.Services.Dto;
using Abrazos.Services.Interfaces;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Models;
using ServicesQueries.Dto;
using System.ComponentModel;
using System.IO;
using Utils;

namespace Abrazos.Services
{
    public class EventQueryService :IEventQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        ILogger _logger;

        public EventQueryService(ApplicationDbContext context, ILogger<UserQueryService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
       
        public async Task<DataCollection<EventDto>> GetAllAsync(
              int page = 1,
            int take = 500,
            string? search = null,
            int? organizerId = null,
            int? CycleId = null,
            int? danceLevel = null,
            int? danceRol = null,
            int? evenType = null,
            int? CityId = null,
            int? countryId = null,
            DateTime? dateCreated = null,
            DateTime? dateFinish = null
            )
        {
            var queryable =  _context.Event
                           .Include(e => e.TypeEvent)
                           //.Include(perm => perm.Cycle)
                           .Include(a => a.Address)
                               .ThenInclude(c => c.City)
                                    .ThenInclude(co => co.Country)
                           .Include(u=> u.UserCreator)
                           .Include(l => l.Level)
                           .Include(tye => tye.Rol)

                  //.Where(x => (search == null || !search.Any() || search.Contains(x.Name))
                  //              || (search == null || !search.Any() || search.Contains(x.CycleEvents.FirstOrDefault().Cycle.CycleTitle)))
                  ////.Where(x => userName == null || !userName.Any() || userName.Contains(x.UserName))
                  //.Where(x => organizerId == null || (x.UserIdCreator_FK != null && x.UserIdCreator_FK == organizerId))
                  //.Where(x => CycleId == null || (x.CycleEvents.FirstOrDefault().Cycle != null && x.CycleEvents.FirstOrDefault().Cycle.CycleId == CycleId))
                  //.Where(x => danceLevel == null || (x.LevelId != null && x.LevelId == danceLevel))
                  //.Where(x => danceRol == null || (x.RolId != null && x.RolId == danceRol))
                  //.Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))
                  //.Where(x => CityId == null || (x.Address.City.CountryId_FK != null && x.Address.City.CountryId_FK == CityId))
                  //.Where(x => countryId == null || (x.Address.City.Country.CountryId != null && x.Address.City.Country.CountryId == countryId))
                  //.Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))
                  //agregar que el estado d ela cakllle este activado
                  .OrderByDescending(x => x.Name);

            var events = await queryable.GetPagedAsync(page, take);


            _logger.LogInformation(queryable.ToString());


            var eventsOutput = new DataCollection<EventDto>
            {
                Items = events.Items.Select(e => new EventDto
                {


                    EventId = e.EventId,
                    UserIdCreator = e.UserIdCreator,
                    UserName = e.UserCreator?.Name,
                    Name = e.Name,
                    Description = e.Description,
                    AddressId = e.AddressId,
                    Image = e.Image,
                    DateInit = e.DateInit,
                    DateFinish = e.DateFinish,
                    EventStateId = e.EventStateId,
                    EventStateName = e.EventState.Name,
                    TypeEventId = e.TypeEventId,
                    TypeEventName = e.TypeEvent.Name,

                    Cupo = e.Cupo,
                    RolId = e.RolId,
                    LevelId = e.LevelId,
                    LevelName = e.Level?.Name,
                    RolName = e.Rol?.Name,

                    Address =  new AddressDto
                    {
                        AddressId = e.AddressId,
                        UserId = e.UserIdCreator,
                        CityId = e.Address.CityId,
                        StateAddress = e.Address.StateAddress,
                        Street = e.Address.Street,
                        Number = e.Address.Number,
                        DetailAddress = e.Address.DetailAddress,
                        CountryId = e.Address.City.CountryId,
                        CountryName = e.Address.City.Country.Name
                    },

                }).ToList()
                };

            return eventsOutput;
        }

    }
}


