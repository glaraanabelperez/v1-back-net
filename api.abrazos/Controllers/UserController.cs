using Abrazos.Services.Interfaces;
using Abrazos.ServicesEvenetHandler.Intefaces;
using api.abrazos.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceEventHandler.Command.CreateCommand;

namespace api.abrazos.Controllers
{
    [ApiController]
    [Route("v1/users")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryService _userService;
        private readonly IUserCommandService _userCommandHandler;

        public UserController(IUserQueryService IUserService, IUserCommandService userCommandHandler)
        {
            _userService = IUserService;
            _userCommandHandler = userCommandHandler;
        }


        /// <summary>
        /// Return All Users by some filters. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        /// <param name="userStates"></param>
        /// <param name="cityId"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            int page = 1,
            int take = 500,
            string? name = null,
            string? userName = null,
            bool? userStates = null,
            int? cityId = null,
            string? countryId = null
        )
        {

            var users = await _userService.GetAllAsync(
                                                   page = 1,
                                                   take = 500,
                                                   name = null,
                                                   userName = null,
                                                   userStates = null,
                                                   cityId = null,
                                                   countryId = null
                                                );
            return users!=null
                    ? Ok(users)
                    : NoContent();
        }

        /// <summary>
        /// Return All Users by some filters. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        /// <param name="userStates"></param>
        /// <param name="cityId"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ActiveUsersProfiles")]
        public async Task<IActionResult> GetAllActiveUsersProfiles(
            int page = 1,
            int take = 500,
            string? name = null,
            string? userName = null,
            int? danceLevel = null,
            int? danceRol = null,
            int? evenType = null,
            int? cityId = null,
            string? countryId = null
        )
        {

            var users = await _userService.GetAllUserProfileAsync(
                                                   page,
                                                   take,
                                                   name,
                                                   userName,
                                                   danceLevel,
                                                   danceRol,
                                                   evenType,
                                                   cityId,
                                                   countryId 
                                                );
            return users != null
                    ? Ok(users)
                    : NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser(UserUpdateCommand User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userCommandHandler.UpdateUser(User);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }
        [HttpPost]
        public async Task<IActionResult> Add(UserCreateCommand User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userCommandHandler.AddUser(User);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }

        /// <summary>
        /// Return Evenet by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getUserIdFirebase")]
        public async Task<IActionResult> GetAsyncIdFirebase([FromQuery] string userIdFirebase)
        {
            //Mas adelante se usara el userId-
            var user = await _userService.GetAsyncIdFirebase(userIdFirebase);

            return user != null
            ? Ok(user)
            : StatusCode(204);

        }

        [HttpGet]
        [Route("getUserId")]
        public async Task<IActionResult> GetAsyncUserId([FromQuery] int userId)
        {
            var event_ = await _userService.GetAsyncUserId(userId);

            return event_ != null
            ? Ok(event_)
            : StatusCode(204);

        }

        /// <summary>
        /// Delete user by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(int userId)
        {

            var result = await _userCommandHandler.DeleteAsync(userId);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }
    }
}