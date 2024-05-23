using Abrazos.Persistence.Database;
using Abrazos.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using ServicesQueries.Dto;
using System;
using Utils;

namespace Abrazos.Services
{
    public class ProfileDancerQueryService : IProfileDancerQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        ILogger _logger;

        public ProfileDancerQueryService(ApplicationDbContext context, ILogger<ProfileDancerQueryService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }     

        public async Task<ProfileDancerDto> GatAsync(int userId)
        {

            var queryable = await _context.ProfileDancer
                                        .Include(u => u.User)
                                        .Include(x => x.DanceRol)
                                        .Include(x => x.DanceLevel)
                                        .SingleOrDefaultAsync(x => x.UserId == userId);
                              
            //_logger.LogWarning(queryable.ToString());
            return _mapper.Map<ProfileDancerDto>(queryable);
 
        }

        public DataCollection<UserProfileDto> mapToProfileDamcerDto(DataCollection<User> query)
        {
            DataCollection<UserProfileDto> profiles = new DataCollection<UserProfileDto>();
            profiles.Total = query.Total;
            profiles.Page = query.Page;
            profiles.Items =
                query.Items.Select(x => new UserProfileDto()
                {
                      UserId = x.UserId,
                      Name = x.Name,
                      LastName= x.LastName,
                      UserName = x.UserName,
                      AvatarImage = x.AvatarImage,
                      UserState = x.UserState,
                      ProfileDancer = x.ProfileDancer.Select(x => new ProfileDancerDto()
                      {
                            ProfileDanceId =x.ProfileDanceId,
                            DanceLevelId =x.DanceLevelId,
                            DanceRolId =x.DanceRol.DanceRolId,
                            DanceLevelName = x.DanceLevel.Name,
                            Height = x.Height,
                            Experience = x.Experience,
                            DanceId = x.Dance.DanceId,
                            DanceName = x.Dance.Name
                        }).ToList(),
                      Userlanguages= x.Userlanguages.Select(x => new UserLanguageDto()
                      {
                            UserLanguageId = x.UserLanguageId,
                            LanguageId = x.LanguageId,
                            LanguageName = x.Language.Name
                       }).ToList(),
                }).ToList();

            return profiles;
        }

    }
}