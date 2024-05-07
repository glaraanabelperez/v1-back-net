using ServicesQueries.Dto;
using Utils;

namespace Abrazos.Services.Interfaces
{
    public interface ICountryQueryService
    {
        public  Task<DataCollection<CountryDto>> GetAllCountryWithEvents(int page = 1,int take = 500);

        public  Task<CountryDto> GetCountryByName(string countryName);


    }

}
    