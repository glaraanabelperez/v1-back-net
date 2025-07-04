

using Abrazos.Persistence.Database;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command.CreateCommand;
using Utils;

namespace Abrazos.ServiceEventHandler
{
    public class InscriptionsCommandService : IInscriptionsCommandService
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public IGenericRepository commandGeneric;

        public InscriptionsCommandService(ApplicationDbContext dbContext, IGenericRepository command,
            ILogger<InscriptionsCommandService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            this.commandGeneric = command;
            _mapper = mapper;
            _logger = logger;
        }

        public bool IsDifferenceValid(bool withCouple, int? num1 = 0, int? num2 = 0)
        {
            var rest = withCouple ? 2 : 1;

            return ((decimal)num1 - (decimal)num2) >= rest;
        }

        public async Task<ResultApp> AddInscriptionAsync(InscriptionCommandCreate inscription)
        {
            using (IDbContextTransaction transac = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    ResultApp res = new ResultApp();
                    var inscriptionValidate = false;

                    var event_ = _dbContext.Event
                                        .Include(e => e.Level)
                                        .Include(e => e.Rol)
                                        .Where(x => x.EventId == inscription.EventId)
                                        .SingleOrDefault();

                    var Profile = _dbContext.ProfileDancer
                                            .Include(e => e.DanceLevel)
                                            .Include(e => e.DanceRol)
                                            .Where(x => x.ProfileDanceId == inscription.ProfileDancerId)
                                            .SingleOrDefault();

                    if (event_ != null && !event_.Deleted && event_.EventStateId == 1)
                    {
                        if (Profile == null)
                        {
                            throw new Exception("No existe el Perfil de bailarin");
                        }

                        //Validation Level if exist-
                        if (event_.LevelId != null && Profile.DanceLevel.LevelNumber < event_.Level.LevelNumber)
                        {
                            throw new Exception("Para este evento necesita tener minimo un nivel " + event_.Level.Name);
                        }

                        // Without couple-
                        if (!event_.Couple)
                        {
                            // Validate cupo-
                            if (event_.Cupo != null && !IsDifferenceValid(event_.Cupo, event_.Inscriptions, inscription.InscripcionWithCouple))
                                throw new Exception("No hay cupo disponible.");

                            // Validate Rol-
                            if (event_.RolId != null
                                    && Profile.DanceRol.DanceRolId != event_.RolId && (event_.Rol.Comodin == false & Profile.DanceRol.Comodin == false))
                                throw new Exception("Para este evento necesita tener un Rol " + event_.Rol.Name);

                            event_.InscriptionsConfirm = inscription.InscripcionWithCouple ? event_.InscriptionsConfirm + 2 : event_.InscriptionsConfirm + 1; // With couple the coup is verify with abrazos-
                        }

                        event_.Inscriptions = inscription.InscripcionWithCouple ? event_.Inscriptions + 2 : event_.Inscriptions + 1;

                    }
                    else
                    {
                        throw new Exception("El evento no esta activo o no tiene cupos disponibles");
                    }


                    UserEventInscription inscription_ = new UserEventInscription()
                    {
                        UserId = inscription.UserId,
                        EventId = inscription.EventId,
                        State = true,
                        ProfileDancerId = inscription.ProfileDancerId,
                        Partner = inscription.InscripcionWithCouple

                    };

                    await this.commandGeneric.Update<Event>(event_);

                    var entry = await this.commandGeneric.Add<UserEventInscription>(inscription_);
                    res.Succeeded = true;

                    await transac.CommitAsync();
                    res.Succeeded = true;
                    res.message = "Inscripcion Exitosa";

                    return res;


                }
                catch (Exception ex)
                {
                    await transac.RollbackAsync();
                    throw;
                }

            }


        }

        public async Task<ResultApp> CancelInscriptionAsync(int userIncriptionId, int userId)
        {
            using (IDbContextTransaction transac = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    ResultApp res = new ResultApp();
                    var inscriptionValidate = false;

                    var insc = _dbContext.UserEventInscription
                                        .Include(x => x.Event)
                                        .Where(x => x.UserEventInscriptionId == userIncriptionId && (userId == x.UserId || x.Event.UserIdCreator == userId))
                                        .SingleOrDefault();

                    var couples = _dbContext.CouplesEvent_Date
                                        .Where(x => x.InvitedUserId == userId || x.HostUserId == userId)
                                        .ToList();

                    if (insc == null)
                    {
                        throw new Exception("No existe la inscripcion");
                    }
                    else
                    {
                        //To update cupo and inscriptions
                        var even = _dbContext.Event
                                        .Where(x => x.EventId == insc.EventId)
                                        .SingleOrDefault();

                        even.Inscriptions = insc.Partner ? even.Inscriptions - 2 : even.Inscriptions - 1;


                        if (couples != null)
                        {
                            bool existCouple = couples.Exists(x => x.RequestAccepted);
                            even.Inscriptions = (insc.Partner || existCouple) ? even.Inscriptions - 2 : even.Inscriptions - 1;
                            even.InscriptionsConfirm = (insc.Partner || existCouple) ? even.InscriptionsConfirm - 2 : even.InscriptionsConfirm - 1;

                            //Delete Inscription
                            await this.commandGeneric.Delete<UserEventInscription>(insc);
                            //Delete couple
                            _dbContext.RemoveRange(couples);
                            await _dbContext.SaveChangesAsync();
                            //Update cupo
                            this.commandGeneric.Update<Event>(even);
                        }

                    }


                    res.Succeeded = true;

                    await transac.CommitAsync();
                    res.Succeeded = true;
                    res.message = "Inscripcion Eliminada";

                    return res;


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

