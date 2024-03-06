using Abrazos.Services.Dto;
using ServicesQueries.Auth;
using System.ComponentModel.DataAnnotations;
using Utils;

namespace Abrazos.Services.Interfaces
{
    public interface IEventQueryService
    {
        Task<DataCollection<EventDto>> GetAllAsync(
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
        );

    }

}
    