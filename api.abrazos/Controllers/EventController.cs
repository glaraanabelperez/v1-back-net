using Abrazos.ServiceEventHandler;
using Abrazos.Services.Interfaces;
using Abrazos.ServicesEvenetHandler.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceEventHandler.Command.CreateCommand;

namespace api.abrazos.Controllers
{
    [ApiController]
    [Route("v1/event")]
    //[Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventCommandService command_;
        private readonly IEventQueryService _eventQuery;

        public EventController(IEventCommandService command, IEventQueryService eventQuery)
        {
            command_ = command;
            _eventQuery = eventQuery;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(EventCreateCommand commandCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await command_.Add(commandCreate);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProfileDancerUpdateCommand profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return null;
            //var result = await command_.Update(profile);
            //return result?.Succeeded ?? false
            //        ? Ok(result)
            //        : BadRequest(result?.message);

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
            string? search,
            int? organizerId,
            int? CycleId,
            int? danceLevel,
            int? danceRol,
            int? evenType,
            int? CityId,
            int? addressId,
            int? countryId,
            DateTime? dateCreated,
            DateTime? dateFinish,
            int page = 1,
            int take = 500
        )
        {
            var events = await _eventQuery.GetAllAsync(
                search,
                organizerId,
                CycleId,
                danceLevel,
                danceRol,
                evenType,
                CityId,
                addressId,
                countryId,
                dateCreated,
                dateFinish,
                page,
                take
               );

            return Ok(events);
        }

    }
}