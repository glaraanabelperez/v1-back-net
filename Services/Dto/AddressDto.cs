using Models;

namespace Abrazos.Services.Dto
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public int UserId_FK { get; set; }
        public int CityId_FK { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string DetailAddress { get; set; }
        public bool StateAddress { get; set; }
        public int CountryId_FK { get; set; }
        public string CountryName { get; set; } 


    }
}