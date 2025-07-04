using Abrazos.Services.Interfaces;
using Abrazos.ServicesEvenetHandler.Intefaces;
using api.abrazos.Validators;
using Microsoft.AspNetCore.Mvc;
using ServiceEventHandler.Command.CreateCommand;
using ServiceEventHandler.Command.UpdateCommand;

namespace api.abrazos.Controllers
{
    [ApiController]
    [Route("v1/couple")]
    //[Authorize]
    public class CoupleController : ControllerBase
    {
        private readonly ICoupleCommandService command_;
        private readonly ICoupleQueryService _Query;

        public CoupleController(ICoupleCommandService command, ICoupleQueryService eventQuery)
        {
            command_ = command;
            _Query = eventQuery;
        }

        [Route("send-invite")]
        [HttpPost]
        public async Task<IActionResult> AddCouple(int userHost, int userInvite, int eventId)
        {

            var result = await command_.SendInvite(userHost, userInvite, eventId);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sended"></param>
        /// <param name="recived"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("view-invites")]
        public async Task<IActionResult> GetInvites(
            bool? sended,
            int userId,
            int page = 1,
            int take = 500
        )
        {
            var result = await _Query.GetInvites(
                sended,
                userId,
                page,
                take
               );

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CouplesEventId"></param>
        /// <param name="userId"></param>
        /// <param name="respond"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("respond-invite")]
        public async Task<IActionResult> PatchCouple(
            int CouplesEventId,
            int userId,
            bool respond
        )
        {
            var result = await command_.RespondInvite(
                CouplesEventId,
                userId,
                respond
               );

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CouplesEventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("cancel-couple")]
        public async Task<IActionResult> CancelInvite_Couple(
            int CouplesEventId,
            int userId
        )
        {
            var result = await command_.CalcelCouple(
                CouplesEventId,
                userId
               );

            return Ok(result);
        }
    }
}
