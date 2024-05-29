using Abrazos.Services.Dto;
using ServicesQueries.Auth;
using ServicesQueries.Dto;
using System.ComponentModel.DataAnnotations;
using Utils;

namespace Abrazos.Services.Interfaces
{
    public interface IProfileDancerQueryService
    {
        Task<ProfileDancerDto> GatAsync(int userId);
   
    }

}
    