

using Abrazos.Persistence.Database;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using Utils;

namespace Abrazos.ServiceEventHandler
{
    public class CoupleCommandService : ICoupleCommandService
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public IGenericRepository command;

        public CoupleCommandService(ApplicationDbContext dbContext, IGenericRepository command,
            ILogger<CoupleCommandService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            this.command = command;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultApp> SendInvite(int userHost, int userInvited, int eventId)
        {
            using (IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = new ResultApp();
                    var quey = _dbContext.UserEventInscription
                                            .Include(x => x.Event)
                                            .Include(l => l.Profile)
                                                .ThenInclude(e => e.DanceRol)
                                            .Include(l => l.Profile)
                                                .ThenInclude(e => e.DanceLevel)
                                  .Where(x => (x.UserId == userHost || x.UserId == userInvited) && x.EventId == eventId);



                    if (quey == null || !quey.Any(x => x.UserId == userHost) || !quey.Any(x => x.UserId == userInvited))
                    {
                        result.message = "Los usuario o el evento no se encuentran";
                        result.Succeeded = false;
                        await transaction.CommitAsync();

                        return result;
                    }

                    var invitationRepet = _dbContext.CouplesEvent_Date
                                            .Where(x => (x.HostUserId == userHost && x.InvitedUserId == userInvited) && x.EventId == eventId)
                                            .SingleOrDefault();

                    //verifica que el userHost o UserIvited no tenga ya un compromiso asumido para este evento-
                    CouplesEventDate timeExpirion = await _dbContext.CouplesEvent_Date
                                     .Where(x => x.InvitedUserId == userHost || x.HostUserId == userHost || x.InvitedUserId == userInvited || x.HostUserId == userInvited
                                     && x.RequestAccepted && x.EventId == eventId).FirstOrDefaultAsync();


                    if (timeExpirion != null)
                    {
                        result.message = "El usuario ya no esta disponible para este evento";
                        result.Succeeded = false;
                        await transaction.CommitAsync();

                        return result;
                    }

                    var coupleConrifmation = _dbContext.CouplesEvent_Date
                                                .Where(x => x.RequestAccepted && x.EventId == eventId)
                                                .Count();

                    if (invitationRepet != null)
                    {
                        result.message = "No puede enviar mas de una vez una invitacion a una persona.";
                        result.Succeeded = false;
                        await transaction.CommitAsync();

                        return result;
                    }
                    else
                    {
                        if (quey.First().Event.Cupo != null && !IsDifferenceValidToCouple(quey.First().Event.Cupo, coupleConrifmation))
                        {
                            result.Succeeded = false;
                            result.message = "No hay cupo";
                            await transaction.CommitAsync();

                            return result;
                        }


                        CouplesEventDate couple = new CouplesEventDate()
                        {
                            HostUserId = userHost,
                            InvitedUserId = userInvited,
                            EventId = eventId,
                        };
                        var entry = await this.command.Add<CouplesEventDate>(couple);

                        result.Succeeded = true;
                        result.message = "Invitacion enviada";

                        await transaction.CommitAsync();
                        return result;

                    }
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }
        }

        public bool IsDifferenceValidToCouple(int? num1 = 0, int? num2 = 0)
        {
            return ((decimal)num1 - (decimal)num2) >= 2;
        }

        public async Task<ResultApp> RespondInvite(int CouplesEventId, int userId, bool respond)
        {
            using (IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = new ResultApp();
                    CouplesEventDate quey = await _dbContext.CouplesEvent_Date
                              .Where(x => x.InvitedUserId == userId && x.CouplesEventId == CouplesEventId
                              && !x.RequestAccepted && !x.RequestRejected).SingleOrDefaultAsync();

                    if (quey == null)
                    {
                        result.message = "No se encuentra la invitacion pendiente";
                        result.Succeeded = false;
                        await transaction.CommitAsync();
                        return result;
                    }

                    CouplesEventDate timeExpirion = await _dbContext.CouplesEvent_Date
                              .Where(x => (x.InvitedUserId == userId || x.HostUserId == userId)
                              && x.RequestAccepted && x.EventId == quey.EventId).FirstOrDefaultAsync();


                    if (timeExpirion != null)
                    {
                        result.message = "El usuario ya no esta disponible para este evento";
                        result.Succeeded = false;
                        await transaction.CommitAsync();
                        return result;
                    }

                    var coupleConrifmation = _dbContext.CouplesEvent_Date
                                               .Where(x => x.RequestAccepted && x.EventId == quey.EventId)
                                               .Count();

                    var event_ = await _dbContext.Event
                                .Where(x => x.EventId == quey.EventId).SingleOrDefaultAsync();

                    if (event_.Cupo != null && !IsDifferenceValidToCouple(event_.Cupo, coupleConrifmation))
                    {
                        result.message = "No hay cupo";
                        result.Succeeded = false;
                        await transaction.CommitAsync();
                        return result;
                    }

                    if (respond)
                    {
                        quey.RequestAccepted = true;
                        event_.InscriptionsConfirm = event_.InscriptionsConfirm + 2;
                        await this.command.Update<Event>(event_);
                        result.message = "Invitacion aceptada";                 //notificacion
                    }
                    else
                    {
                        quey.RequestRejected = true;
                        result.message = "Invitacion rechazada";
                    }

                    await this.command.Update<CouplesEventDate>(quey);
                    result.Succeeded = true;
                    await transaction.CommitAsync();
                    return result;

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }
        }

        public async Task<ResultApp> CalcelCouple(int CouplesEventId, int userId)
        {
            var invitation = await _dbContext.CouplesEvent_Date
                        .Where(a => a.CouplesEventId == CouplesEventId)
                        .SingleOrDefaultAsync();

            if (invitation == null)
            {
                return new ResultApp(false, null, "NO existe la invitacion", null);
            }
            var event_ = await _dbContext.Event
                    .Where(x => x.EventId == invitation.EventId).SingleOrDefaultAsync();

            using (IDbContextTransaction transac = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (userId == event_.UserIdCreator || invitation.InvitedUserId == userId || invitation.HostUserId == userId)
                    {
                        await command.Delete(invitation);
                        event_.Inscriptions = event_.Inscriptions - 1;
                        if (event_.Couple && invitation.RequestAccepted)
                            event_.InscriptionsConfirm = event_.InscriptionsConfirm - 1;

                        await command.Update(event_);
                        await transac.CommitAsync();
                        return new ResultApp(true, null, "Abrazo cancelado", null);
                    }

                    await transac.CommitAsync();
                    return new ResultApp(false, null, "El usuario no tiene permiso para cancela el abrazo", null);
                }
                catch (Exception ex)
                {
                    await transac.RollbackAsync();
                    throw;
                }

            }
        }
    }
}


