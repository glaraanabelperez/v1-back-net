

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
using System.Xml.Linq;
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

        /// <summary>
        /// GetAll By Cycle with Filters.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="organizerId"></param>
        /// <param name="CycleId"></param>
        /// <param name="danceLevel"></param>
        /// <param name="danceRol"></param>
        /// <param name="evenType"></param>
        /// <param name="CityId"></param>
        /// <param name="addressId"></param>
        /// <param name="countryId"></param>
        /// <param name="dateCreated"></param>
        /// <param name="dateFinish"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns>DataCollection<EventDto></EventDto></returns>
        public async Task<DataCollection<EventDto>> GetAllAsync(
            string? search,
            int? organizerId,
            int? CycleId,
            int? danceLevel,
            int? danceRol,
            int? evenType,
            int? CityId,
            int? addressId,
            int? countryId,
            DateTime? dateInit,
            DateTime? dateFinish,
            int page = 1,
            int take = 500
            )
        {
            var queryable =  _context.Event
                           .Include(e => e.Cycle)
                           .Include(e => e.TypeEvent)
                           .Include(a => a.Address)
                               .ThenInclude(c => c.City)
                                    .ThenInclude(co => co.Country)
                                .Include(u=> u.UserCreator)
                                .Include(l => l.Level)
                                .Include(tye => tye.Rol)

            //.Where(x => (  search == null || !search.Any() || search.Contains(x.Name) || (x.Cycle!=null && search.Contains(x.Cycle.CycleTitle)) ))
                            .Where(x => (organizerId == null || x.UserIdCreator == organizerId)
                                    && (CycleId == null || x.Cycle.CycleId == CycleId)
                                    && (danceLevel == null || x.LevelId == danceLevel)
                                    && (danceRol == null || x.RolId == danceRol)
                                    && (evenType == null || x.TypeEventId == evenType)
                                    && (CityId == null || x.Address.City.CityId == CityId)
                                    && (addressId == null || x.Address.AddressId== addressId)
                                    && (countryId == null || x.Address.City.Country.CountryId == countryId)
                                    && (evenType == null || x.TypeEventId == evenType)
                                    && (dateInit == null || dateFinish == null || (dateInit >= x.DateInit && x.DateFinish >= dateFinish) )

                            )
            //.Where(x => danceLevel == null || (x.LevelId != null && x.LevelId == danceLevel))
            //.Where(x => danceRol == null || (x.RolId != null && x.RolId == danceRol))
            //.Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))
            //.Where(x => CityId == null || (x.Address.City.CountryId_FK != null && x.Address.City.CountryId_FK == CityId))
            //.Where(x => countryId == null || (x.Address.City.Country.CountryId != null && x.Address.City.Country.CountryId == countryId))
            //.Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))
            //agregar que el estado d ela cakllle este activado
                  .OrderByDescending(x => x.EventId);

            var cycles = _mapper.Map<DataCollection<EventDto>>(
                await queryable.GetPagedAsync(page, take));


            _logger.LogInformation(queryable.ToString());


            //var eventsOutput = new CycleDto
            //{
            //    CycleId = events.
            //    Events = events.Items.Select(e => new EventDto
            //    {


            //        EventId = e.EventId,
            //        UserIdCreator = e.UserIdCreator,
            //        UserName = e.UserCreator?.Name,
            //        Name = e.Name,
            //        Description = e.Description,
            //        AddressId = e.AddressId,
            //        Image = e.Image,
            //        DateInit = e.DateInit,
            //        DateFinish = e.DateFinish,
            //        EventStateId = e.EventStateId,
            //        EventStateName = e.EventState.Name,
            //        TypeEventId = e.TypeEventId,
            //        TypeEventName = e.TypeEvent.Name,

            //        Cupo = e.Cupo,
            //        RolId = e.RolId,
            //        LevelId = e.LevelId,
            //        LevelName = e.Level?.Name,
            //        RolName = e.Rol?.Name,

            //        Address =  new AddressDto
            //        {
            //            AddressId = e.AddressId,
            //            UserId = e.UserIdCreator,
            //            CityId = e.Address.CityId,
            //            StateAddress = e.Address.StateAddress,
            //            Street = e.Address.Street,
            //            Number = e.Address.Number,
            //            DetailAddress = e.Address.DetailAddress,
            //            CountryId = e.Address.City.CountryId,
            //            CountryName = e.Address.City.Country.Name
            //        },

            //    }).ToList()
            //    };

            return cycles;
        }

    }
}


