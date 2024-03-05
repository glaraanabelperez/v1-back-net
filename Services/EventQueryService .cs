

using Abrazos.Persistence.Database;
using Abrazos.Services.Dto;
using Abrazos.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using ServicesQueries.Auth;
using System;
using System.Linq;
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
       
        public async Task<DataCollection<UserDto>> GetAllAsync(
              int page = 1,
            int take = 500,
            string? search = null,
            int? organizerId = null,
            int? CycleId = null,
            int? danceLevel = null,
            int? danceRol = null,
            int? evenType = null,
            int? CityId = null,
            int? addressId = null,
            int? countryId = null,
            DateTime? dateCreated = null,
            DateTime? dateFinish = null
            )
        {
            var queryable = await _context.Event
                           .Include(a => a.TypeEvent_)
                           .Include(perm => perm.CycleEvents)
                           .ThenInclude(a => a.Cycle)
                           .Include(a => a.Address)
                               .ThenInclude(a => a.City)
                               .ThenInclude(a => a.Country)
                           .Include(l => l.Level)
                           .Include(tye => tye.Rol)

                  .Where(x => search == null || !search.Any() || search.Contains(x.Name))
                  //.Where(x => userName == null || !userName.Any() || userName.Contains(x.UserName))
                  .Where(x => organizerId == null || (x.UserIdCreator_FK != null && x.UserIdCreator_FK == organizerId))
                  .Where(x => CycleId == null || (x.CycleEvents.FirstOrDefault().Cycle != null && x.CycleEvents.FirstOrDefault().Cycle.CycleId == CycleId))
                  .Where(x => danceLevel == null || (x.LevelId != null && x.LevelId == danceLevel))
                  .Where(x => danceRol == null || (x.RolId != null && x.RolId == danceRol))
                  .Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))

                  .OrderByDescending(x => x.Name)
                  .GetPagedAsync(page, take);
            //.Skip((page - 1) * take)
            //.Take(take)
            //.ToListAsync();

            _logger.LogInformation(queryable.ToString());

            var result = _mapper.Map<DataCollection<UserDto>>(queryable);


            return result;
        }

    }
}