

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
    public class ProfileDancerService :IprofileDancerQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        ILogger _logger;

        public ProfileDancerService(ApplicationDbContext context, ILogger<UserQueryService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get Filter by, Rol's or Level's in Events, Citys, Countries and search by rol's name or level's level
        /// </summary>
        /// <param name="rolSearchName"></param>
        /// <param name="levelSearchName"></param>
        /// <param name="danceLevelId"></param>
        /// <param name="danceRolId"></param>
        /// <param name="eventId"></param>
        /// <param name="cityId"></param>
        /// <param name="countryId"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<DataCollection<ProfileDancerDto>> GetAllAsync(
            string? rolSearchName, 
            string? levelSearchName, 
            int? danceLevelId, 
            int? danceRolId, 
            int? eventId, 
            int? cityId,
            int? countryId, 
            int page = 1, 
            int take = 500)
        {
            var queryable = _context.ProfileDancer
                                .Include(x => x.DanceLevel)
                                .Include(x => x.DanceRol)
                                .Include(u => u.User)
                                .ThenInclude(u => u.Address)
                                        .ThenInclude(u => u.City)
                                        .ThenInclude(u => u.Country)

            .Where(x => (rolSearchName == null || !rolSearchName.Any() || rolSearchName.Contains(x.DanceRol.Name))
                            && (levelSearchName == null || !levelSearchName.Any() || levelSearchName.Contains(x.DanceLevel.Name))
                            && (danceLevelId == null || x.DanceLevel.DanceLevelId == danceLevelId)
                            && (danceRolId == null || x.DanceRol.DanceRolId == danceRolId)
                            && (cityId == null || x.User.Address.Any(x => x.City.CityId == cityId))
                            && (countryId == null || x.User.Address.Any(x => x.City.CountryId == countryId))
                            && (eventId == null || x.DanceRol.Events.Any(x => x.RolId == danceRolId))
                            && (eventId == null || x.DanceLevel.Events.Any(x => x.LevelId == danceLevelId))
                            )
            .OrderByDescending(x => x.ProfileDanceId);
                            

            var profiles = _mapper.Map<DataCollection<ProfileDancerDto>>(
                await queryable.GetPagedAsync(page, take));

            _logger.LogInformation(queryable.ToString());

            return profiles;
        }


        /// <summary>
        /// GetById
        /// </summary>
        /// <returns>EventDto</returns>
        public async Task<ProfileDancerDto> GetAsync(int profileDanceId)
        {
            var entity = _context.ProfileDancer
                              .SingleOrDefault(x => x.ProfileDanceId==profileDanceId);

         


            var entityDto = _mapper.Map<ProfileDancerDto>(entity);

            _logger.LogInformation(entity.ToString());

            return entityDto;
        }

    }
}


