

using Abrazos.Persistence.Database;
using Abrazos.Services.Dto;
using Abrazos.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using ServicesQueries.Auth;
using System;
using System.Linq;
using System.Xml.Linq;
using Utils;
using static System.Net.Mime.MediaTypeNames;

namespace Abrazos.Services
{
    public class EventQueryService :IEventQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        ILogger _logger;

        public EventQueryService(ApplicationDbContext context, ILogger<UserQueryService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
       
        public async Task<DataCollection<EventDto>> GetAllAsync(
              int page = 1,
            int take = 500,
            string? search = null,
            int? organizerId = null,
            int? CycleId = null,
            int? danceLevel = null,
            int? danceRol = null,
            int? evenType = null,
            int? CityId = null,
            int? countryId = null,
            DateTime? dateCreated = null,
            DateTime? dateFinish = null
            )
        {
            var events = await _context.Event
                           .Include(a => a.TypeEvent_)
                           .Include(perm => perm.CycleEvents)
                           .ThenInclude(a => a.Cycle)
                           .Include(a => a.Address)
                               .ThenInclude(a => a.City)
                               .ThenInclude(a => a.Country)
                           .Include(l => l.Level)
                           .Include(tye => tye.Rol)

                  .Where(x => ( search == null || !search.Any() || search.Contains(x.Name) ) 
                                || (search == null || !search.Any() || search.Contains(x.CycleEvents.FirstOrDefault().Cycle.CycleTitle)) )
                  //.Where(x => userName == null || !userName.Any() || userName.Contains(x.UserName))
                  .Where(x => organizerId == null || (x.UserIdCreator_FK != null && x.UserIdCreator_FK == organizerId))
                  .Where(x => CycleId == null || (x.CycleEvents.FirstOrDefault().Cycle != null && x.CycleEvents.FirstOrDefault().Cycle.CycleId == CycleId))
                  .Where(x => danceLevel == null || (x.LevelId != null && x.LevelId == danceLevel))
                  .Where(x => danceRol == null || (x.RolId != null && x.RolId == danceRol))
                  .Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))
                  .Where(x => CityId == null || (x.Address.City.CountryId_FK != null && x.Address.City.CountryId_FK == CityId))
                  .Where(x => countryId == null || (x.Address.City.Country.CountryId != null && x.Address.City.Country.CountryId == countryId))
                  .Where(x => evenType == null || (x.TypeEvent_ != null && x.TypeEvent_.TypeEventId == evenType))

                  .OrderByDescending(x => x.Name)
                  .GetPagedAsync(page, take);


            _logger.LogInformation(events.ToString());

            //var result = _mapper.Map<DataCollection<EventDto>>(queryable);


            var eventsOutput = new DataCollection<EventDto>
            {
                Items = events.Items.Select(e => new EventDto
                {
                    EventId = e.EventId,
                    UserIdCreator_FK = e.UserIdCreator_FK,
                    Name = e.Name,
                    Description = e.Description,
                    AddressId_fk = e.AddressId_fk, 
                    Image = e.Image, 
                    DateInit = e.DateInit,
                    DateFinish = e.DateFinish, 
                    EventStateId_fk = e.EventStateId_fk,
                    TypeEventId_fk =e.TypeEventId_fk,

                    Cupo = e.Cupo, 
                    RolId = e.RolId, 
                    LevelId = e.LevelId, 
                    LevelName = e.Level.Name,
                    RolName = e.Rol.Name,
                    EventStateName = e.EventState_.Name,
                    TypeEventName   = e.TypeEvent_.Name,
                    UserCreatorName = e.UserCreator.Name,
                    UserCreatorLastName = e.UserCreator.LastName,

                    Address =
                    CouplesEvents =
                    CycleEvents = 
                            Customer = new CustomerDto
                    {
                        CustomerId = order.OrderSources.FirstOrDefault()?.externalCustomerId,
                        Email = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.customerEmail,
                        Name = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.customerName,
                        LastName = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.customerLastName,
                        Tel = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.customerPhone,
                        BirthDate = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.customerBirthDate,
                        Doc = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.customerDocumentNumber,
                        Gender = order.OrderCustomer.FirstOrDefault()?.META_CUS_Customer?.Gender
                    },
                    Delivery = new DeliveryDto
                    {
                        Street = order.OrderCustomer.FirstOrDefault()?.META_FCO_Address?.addressStreet ?? string.Empty,
                        Number = order.OrderCustomer.FirstOrDefault()?.META_FCO_Address?.addressNumber ?? string.Empty,
                        Floor = order.OrderCustomer.FirstOrDefault()?.META_FCO_Address?.addressFloor ?? string.Empty,
                        Province = order.OrderCustomer.FirstOrDefault()?.META_FCO_Address?.addressProvince ?? string.Empty,
                        City = order.OrderCustomer.FirstOrDefault()?.META_FCO_Address?.addressCity ?? string.Empty,
                        ZipCode = order.OrderCustomer.FirstOrDefault()?.META_FCO_Address?.addressZipCode ?? string.Empty
                    },
                    ItemsDto = order.OrderDetails.Select(
                            orderDetail => new ItemDto
                            {
                                comboProd = orderDetail.DetailCombos.FirstOrDefault()?.comboId != null ? _key.Combo : _key.Producto,
                                codigoCombo = orderDetail.DetailCombos.FirstOrDefault()?.comboCodigoCombo ?? _key.CodeComboNull,
                                codigoSAP = orderDetail.productSAPID,
                                quantity = orderDetail.OrderDetailLog.FirstOrDefault().orderDetailQuantity,
                                vrkme = orderDetail.OrderDetailLog.FirstOrDefault().unitMeasurementText,
                                listPrice = orderDetail.OrderDetailLog.FirstOrDefault().orderDetailNetPrice,
                                discount = orderDetail.OrderDetailDiscounts.FirstOrDefault()?.orderDiscountPercentage ?? 0,
                                price = orderDetail.OrderDetailLog.FirstOrDefault().orderDetailBasePrice,
                            }).ToList(),
                }).ToList(),
                Total = orders.Total,
                Page = orders.Page,

            };



            return eventsOutput;
        }

    }
}


