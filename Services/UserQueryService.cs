using Abrazos.Persistence.Database;
using Abrazos.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServicesQueries.Dto;
using System;
using Utils;

namespace Abrazos.Services
{
    public class UserQueryService :IUserQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        ILogger _logger;

        public UserQueryService(ApplicationDbContext context, ILogger<UserQueryService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
       
        public async Task<DataCollection<UserDto>> GetAllAsync(
            int page = 1, 
            int take = 500, 
            string? name = null, 
            string? userName = null, 
            bool? userStates = null, 
            int? danceLevel = null,
            int? danceRol = null,
            int? evenType = null
            )
        {
            var queryable = await _context.User
                           .Include(a => a.UserPermissions)
                               .ThenInclude(perm => perm.Permission)
                           .Include(a => a.ProfileDancer)
                               .ThenInclude(details => details.DanceRol)
                           .Include(a => a.ProfileDancer)
                               .ThenInclude(details => details.DanceLevel)
                            .Include(tyeu => tyeu.TypeEventsUsers)
                                .ThenInclude(tye => tye.TypeEvent)

                  .Where(x => name == null || !name.Any() || name.Contains(x.Name))
                  .Where(x => userName == null || !userName.Any() || userName.Contains(x.UserName))
                  .Where(x => userStates == null || (x.UserState != null && x.UserState == userStates))
                  .Where(x => danceLevel == null || (x.ProfileDancer.First().DanceLevel != null && x.ProfileDancer.First().DanceLevel.DanceLevelId == danceLevel))
                  .Where(x => danceRol == null || (x.ProfileDancer.First().DanceRol != null && x.ProfileDancer.First().DanceRol.DanceRolId == danceRol))
                  .Where(x => evenType == null || (x.TypeEventsUsers != null && x.TypeEventsUsers.First().TypeEvent.TypeEventId == evenType))

                  .OrderByDescending(x => x.Name)
                  .GetPagedAsync(page, take);

            _logger.LogInformation(queryable.ToString());

            var result = _mapper.Map<DataCollection<UserDto>>(queryable);


            return result;
        }

        public async Task<UserDto> GatAsync(long userId)
        {

            var queryable = (await _context.User
                          .Include(a => a.UserPermissions)
                              .ThenInclude(perm => perm.Permission)
                          .Include(a => a.ProfileDancer)
                              .ThenInclude(details => details.DanceRol)
                          .Include(a => a.ProfileDancer)
                              .ThenInclude(details => details.DanceLevel)
                          .Include(tyeu => tyeu.TypeEventsUsers)
                            .ThenInclude(tye => tye.TypeEvent)
                        .Include(ad => ad.Address)
            .SingleOrDefaultAsync(x => x.UserId == userId));
            _logger.LogWarning(queryable.ToString());
            return _mapper.Map<UserDto>(queryable);
 
        }

        public async Task<ResultApp> LoginAsync(string email, string pass)
        {

            ResultApp res = new ResultApp();
            try
            {
                var queryable = await _context.User
                                     .Where(x => x.Email == email)
                                     .Where(x => x.Pass == pass)
                                     .SingleOrDefaultAsync();

                if (queryable != null)
                {
                    res.Succeeded = true;
                    res.message = "Successful login";
                    return res;
                }
                else
                {
                    res.Succeeded = false;
                    res.message = "UnSuccessful login";
                    return res;
                }

                //_logger.LogWarning(res_.Metadata.ToString());

            }
            catch (System.Exception ex)
            {
                string exMessage = ex.InnerException != null ? ex.InnerException!.Message : ex.Message;
                _logger.LogWarning(exMessage);
                res.Succeeded = false;
                res.message = "Error in Login";
                res.errors = new ErrorResult
                {
                    type = ex.GetType().Name,
                    title = exMessage,
                    status = "500",
                    traceId = Guid.NewGuid().ToString(),
                    errors = new Dictionary<string, List<string>>
                    {
                            {"GetError : Failed to Get User ", new List<string> { exMessage }}
                        }
                };

                return res;
            }

        }


    }
}