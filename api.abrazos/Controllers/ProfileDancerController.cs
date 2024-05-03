using Abrazos.ServiceEventHandler;
using Abrazos.Services.Interfaces;
using Abrazos.ServicesEvenetHandler.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command.CreateCommand;

namespace api.abrazos.Controllers
{
    [ApiController]
    [Route("v1/profileDancer")]
    //[Authorize]
    public class ProfileDancerController : ControllerBase
    {
        private readonly IProfileDancerCommandService _queryCommand;
        private readonly IprofileDancerQueryService _queryService;

        
        public ProfileDancerController(IProfileDancerCommandService queryCommand, IprofileDancerQueryService queryService)
        {
            _queryCommand = queryCommand;
            _queryService = queryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProfileDancerCreateCommand profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _queryCommand.Add(profile);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAsync(ProfileDancerUpdateCommand profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _queryCommand.Update(profile);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }

        /// <summary>
        /// Return All Users by some filters.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        /// <param name="userStates"></param>
        /// <param name="danceLevel"></param>
        /// <param name="danceRol"></param>
        /// <param name="evenType"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? rolSearchName,
            string? levelSearchName,
            int? danceLevelId,
            int? danceRolId,
            int? eventId,
            int? cityId,
            int? countryId,
            int page = 1,
            int take = 500
        )
        {
            var events = await _queryService.GetAllAsync(
               rolSearchName,
               levelSearchName,
               danceLevelId,
               danceRolId,
               eventId,
               cityId,
               countryId,
               page = 1,
               take = 500
               );

            return Ok(events);
        }


        /// <summary>
        /// Return Evenet by Id.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetAsync(int profileId)
        {
            var event_ = await _queryService.GetAsync(profileId);
            return event_ != null
            ? Ok(event_)
            : StatusCode(204);

        }
    }
}