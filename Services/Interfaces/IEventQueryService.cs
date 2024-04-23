using ServicesQueries.Dto;
using Utils;

namespace Abrazos.Services.Interfaces
{
    public interface IEventQueryService
    {
        Task<DataCollection<EventDto>> GetAllAsync(
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
        );

        Task<EventDto> GetAsync(int eventId);


    }

}
    