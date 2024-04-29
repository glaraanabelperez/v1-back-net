using ServicesQueries.Dto;
using Utils;

namespace Abrazos.Services.Interfaces
{
    public interface IprofileDancerQueryService
    {
        Task<DataCollection<ProfileDancerDto>> GetAllAsync(
            string? rolSearchName,
            string? levelSearchName,
            int? danceLevelId,
            int? danceRolId,
            int? eventId,
            int? cityId,
            int? countryId,
            int page = 1,
            int take = 500
        );

        Task<ProfileDancerDto> GetAsync(int eventId);


    }

}
    