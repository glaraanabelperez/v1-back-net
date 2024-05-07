using ServicesQueries.Dto;
using Utils;

namespace Abrazos.Services.Interfaces
{
    public interface ICityQueryService
    {
        public Task<DataCollection<CityDto>> GetAllCityWithEventsByCountry(int? countryId);
        public Task<CityDto> GetCityByName(string cityName);


    }

}
    