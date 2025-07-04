

using Abrazos.Persistence.Database;
using Abrazos.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using ServicesQueries.Dto;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Abrazos.Services
{
    public class InscriptionsQueryService : IInscriptionsQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        ILogger _logger;

        public InscriptionsQueryService(ApplicationDbContext context, ILogger<InscriptionsQueryService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<AllInscriptionsDto> GetInscriptionsToSendInvite(int userId, int eventId, int page = 1, int take = 500)
        {
            AllInscriptionsDto rolsComplementary = null;
            IQueryable<UserEventInscription> listUsers = null;

            var users = _context.UserEventInscription
                                       .Include(e => e.User)
                                       .Include(x => x.Event)
                                       .Include(l => l.Profile)
                                           .ThenInclude(e => e.DanceRol)
                                       .Include(l => l.Profile)
                                           .ThenInclude(e => e.DanceLevel)
                                        .Where(x => x.EventId == eventId);

            var userHost = users.Where(x => x.UserId == userId).FirstOrDefault();

            if (userHost == null)
            {
                throw new Exception("El usuario no se encuentra inscripto en el evento");
            }
            if (userHost.Event.Deleted && userHost.Event.TypeEventId != 1 && userHost.Event.Couple)
            {
                throw new Exception("El evento no se encuentra disponible o no clasifica como clase para enviar invitaciones");
            }

            if (userHost.Profile.DanceRol.Comodin || userHost.Event.UserIdCreator == userId)
                listUsers = users.Where(x => x.UserId != userId);
            else
                listUsers = users.Where(x => x.UserId != userId && x.Profile.DanceRolId != userHost.Profile.DanceRolId);

            rolsComplementary = MapToEventInscriptionDto(await listUsers.GetPagedAsync(page, take), userId);
            _logger.LogInformation(listUsers.ToString());

            return rolsComplementary;
        }

        public AllInscriptionsDto MapToEventInscriptionDto(DataCollection<UserEventInscription> eventinsc_, int userId)
        {
            if (eventinsc_.HasItems)
            {
                AllInscriptionsDto insc = new AllInscriptionsDto();
                insc.EventId = eventinsc_.Items.First().Event.EventId;
                insc.Name = eventinsc_.Items.First().Event.Description;
                insc.Description = eventinsc_.Items.First().Event.Description;
                insc.Inscriptions = eventinsc_.Items.First().Event.Inscriptions;
                if (eventinsc_.Items.First().Event.Cupo != null)
                {
                    insc.Cupo = eventinsc_.Items.First().Event.Cupo;
                }
                insc.Couple = eventinsc_.Items.First().Event.Couple;
                insc.DanceLevelEvent = eventinsc_.Items.First().Event.Level != null ? eventinsc_.Items.First().Event.Level.Name : null;
                insc.DanceRolEvent = eventinsc_.Items.First().Event.Rol != null ? eventinsc_.Items.First().Event.Rol.Name : null;

                if (userId == eventinsc_.Items.First().Event.UserIdCreator)
                {
                    if (eventinsc_.Items.First().Event.CouplesEvents.Count > 0)
                    {
                        List<CouplesEventDateDto> couples = new List<CouplesEventDateDto>();

                        foreach (var c in eventinsc_.Items.First().Event.CouplesEvents)
                        {
                            var couple = new CouplesEventDateDto()
                            {
                                CouplesEventId = c.CouplesEventId,
                                CouplesEventApproved = c.CouplesEventApproved,
                                RequestAccepted = c.RequestAccepted,
                                RequestRejected = c.RequestRejected,
                                HostUser = new UserCoupleDto()
                                {
                                    UserId = c.HostUserId,
                                    Name = c.HostUser.Name,
                                    LastName = c.HostUser.LastName,

                                },
                                InvitedUser = new UserCoupleDto()
                                {
                                    UserId = c.InvitedUserId,
                                    Name = c.InvitedUser.Name,
                                    LastName = c.InvitedUser.LastName
                                },

                            };
                            couples.Add(couple);
                        }
                        insc.couples = couples;
                    }
                }
                List<UserInscDto> listUsers = new List<UserInscDto>();

                foreach (var e in eventinsc_.Items)
                {
                    var userIn = new UserInscDto()
                    {
                        UserEventInscriptionId = e.UserEventInscriptionId,
                        UserId = e.UserId,
                        Partner = e.Partner,
                        Name = e.User.Name,
                        LastName = e.User.LastName,
                        UserName = e.User.UserName,
                        AvatarImage = e.User.AvatarImage,
                        ProfileDancer = new ProfileDancerDto()
                        {
                            ProfileDanceId = e.Profile.ProfileDanceId,
                            DanceRol = new DanceRolDto()
                            {
                                DanceRolId = e.Profile.DanceRolId,
                                Name = e.Profile.DanceRol.Name
                            },
                            DanceLevel = new DanceLevelDto()
                            {
                                DanceLevelId = e.Profile.DanceLevelId,
                                Name = e.Profile.DanceLevel.Name
                            }
                        }
                    };
                    listUsers.Add(userIn);
                }
                insc.listUserInscto = listUsers;
                return insc;
            }
            else return null;

        }

        public async Task<DataCollection<MyInscriptionsDto>> MyInscriptions(int UserId, int page = 1, int take = 500)
        {

            var queryable = _context.UserEventInscription
                                        .Include(l => l.Profile)
                                           .ThenInclude(e => e.DanceRol)
                                        .Include(l => l.Profile)
                                           .ThenInclude(e => e.DanceLevel)
                                       .Include(x => x.Event)
                                            .ThenInclude(l => l.Rol)
                                        .Include(x => x.Event)
                                            .ThenInclude(l => l.Level)


                                        .Where(x => x.UserId == UserId);

            var events = MapToMyEvenetsInscriptions(
               await queryable.GetPagedAsync(page, take));

            return events;
        }
        public DataCollection<MyInscriptionsDto> MapToMyEvenetsInscriptions(DataCollection<UserEventInscription> eventinsc_)
        {
            List<MyInscriptionsDto> list = new List<MyInscriptionsDto>();
            DataCollection<MyInscriptionsDto> listCollection = new DataCollection<MyInscriptionsDto>();

            if (eventinsc_.HasItems)
            {
                foreach (var e in eventinsc_.Items)
                {
                    var insc = new MyInscriptionsDto();

                    insc.EventId = e.Event.EventId;
                    insc.Name = e.Event.Description;
                    insc.Description = e.Event.Description;
                    insc.Inscriptions = e.Event.Inscriptions;
                    if (e.Event.Cupo != null)
                    {
                        insc.Cupo = e.Event.Cupo;
                    }
                    insc.Couple = e.Event.Couple;
                    insc.DanceLevelEvent = e.Event.Level != null ? e.Event.Level.Name : null;
                    insc.DanceRolEvent = e.Event.Rol != null ? e.Event.Rol.Name : null;
                    insc.user = new UserInscDto()
                    {
                        UserEventInscriptionId = e.UserEventInscriptionId,
                        UserId = e.UserId,
                        Partner = e.Partner,
                        ProfileDancer = new ProfileDancerDto()
                        {
                            UserId = e.UserId,
                            DanceRol = new DanceRolDto()
                            {
                                Name = e.Profile.DanceRol.Name
                            },
                            DanceLevel = new DanceLevelDto()
                            {
                                Name = e.Profile.DanceLevel.Name
                            }
                        }
                    };
                    list.Add(insc);
                }
                listCollection.Items = list;
                listCollection.Page = eventinsc_.Page;
                listCollection.Total = eventinsc_.Total;
                listCollection.Pages = eventinsc_.Pages;
                return listCollection;
            }
            else return null;

        }

        public async Task<AllInscriptionsDto> GetAllInscriptions(int userId, int eventId, int page = 1, int take = 500)
        {
            AllInscriptionsDto allusers = null;

            var users = _context.UserEventInscription
                                        .Include(e => e.User)
                                         .Include(l => l.Profile)
                                            .ThenInclude(e => e.DanceRol)
                                        .Include(l => l.Profile)
                                            .ThenInclude(e => e.DanceLevel)
                                        .Include(x => x.Event)
                                        .ThenInclude(x => x.CouplesEvents)
                                        .Where(x => x.EventId == eventId);

            var userPerm = users.Where(x => x.Event.UserIdCreator == userId).FirstOrDefault();

            if (userPerm == null)
            {
                throw new Exception("El usuario no tiene permisos");
            }

            allusers = MapToEventInscriptionDto(await users.GetPagedAsync(page, take), userId);
            _logger.LogInformation(users.ToString());

            return allusers;
        }

    }
}


