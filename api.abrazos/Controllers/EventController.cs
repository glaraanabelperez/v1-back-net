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
        private readonly IEventCommandHandler command_;

        public EventController(IEventCommandHandler command)
        {
            command_ = command;
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

            var users = await _userService.GetAllAsync(
               int page = 1,
            int take = 500,
            string ? search = null,
            int ? organizerId = null,
            int ? CycleId = null,
            int ? danceLevel = null,
            int ? danceRol = null,
            int ? evenType = null,
            int ? CityId = null,
            int ? addressId = null,
            int ? countryId = null,
            DateTime ? dateCreated = null,
            DateTime ? dateFinish = null
               );

            return Ok(users);
        }

    }
}