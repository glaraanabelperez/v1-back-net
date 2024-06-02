

using Abrazos.Persistence.Database;
using Abrazos.Services.Dto;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command;
using ServiceEventHandler.Command.CreateCommand;
using ServiceEventHandler.Validators;
using System.Data.Entity;
using System.Net.NetworkInformation;
using Utils;
using Utils.Exception;

namespace Abrazos.ServiceEventHandler
{
    public class ProfileDancerCommandService: IProfileDancerCommandService
    {

        private readonly ILogger<ProfileDancerCommandService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public IGenericRepository command;

        public ProfileDancerCommandService(ApplicationDbContext dbContext, IGenericRepository command,
           ILogger<ProfileDancerCommandService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            this.command = command;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultApp> Add(ProfileDancerCreateCommand command)
        {
            //Buscar si la direccion existe en la bbdd para traer el id, sino crearlo
            ResultApp res = new ResultApp();

            using (IDbContextTransaction transac = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    User user = _dbContext.User.FirstOrDefault(u => u.UserId == command.UserId);
                    if (user != null)
                    {
                        // AddRange is not in Generci Repository.
                        _dbContext.AddRange(MapToEntity(command));
                        _dbContext.SaveChanges();
                        await transac.CommitAsync();
                        res.Succeeded = true;
                    }
                    else
                    {
                        res.errors = null;
                        res.Succeeded = false;
                        res.message = "El usuario no existe";
                    }
                    
                }
                catch (System.Exception ex)
                {
                    await transac.RollbackAsync();
                    throw;
                }
            }
            return res;

        }

        public ICollection<ProfileDancer> MapToEntity(ProfileDancerCreateCommand command_)
        {
            ICollection<ProfileDancer> profiles = new List<ProfileDancer>();

            foreach (var e in command_.DancerSkils)
            {
                ProfileDancer entity = new ProfileDancer();
                entity.UserId = command_.UserId;
                entity.DanceRolId = e.DanceRolId;
                entity.DanceLevelId = e.DanceLevelId;
                profiles.Add(entity);
            }

            return profiles;
        }


        public async Task<ResultApp> Update(ProfileDancerUpdateCommand command)
        {
            ResultApp res = new ResultApp();
 
                try
                {
                    ProfileDancer profile = _dbContext.ProfileDancer.FirstOrDefault(u => u.ProfileDanceId == command.ProfileDancerId);

                    if (profile != null)
                    {

                        await this.command.Update<ProfileDancer>(MapToUpdateEntity(profile, command));
                        res.Succeeded = true;
                    }
                    else
                    {
                        res.Succeeded = false;
                        res.message = "El usuario no tiene este perfil asociado";
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            return res;
                
        }

        public async Task<ResultApp> DeleteAsync(int profileDancerId)
        {
            ResultApp res = new ResultApp();

                try
                {
                    ProfileDancer profile = _dbContext.ProfileDancer.SingleOrDefault(u => u.ProfileDanceId == profileDancerId);

                    if (profile != null)
                    {

                        await this.command.Delete<ProfileDancer>(profile);
                        res.Succeeded = true;
                    }
                    else
                    {
                        res.Succeeded = false;
                        res.message = "El perfil no se encuentra";
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            return res;

        }

        public ProfileDancer MapToUpdateEntity(ProfileDancer profile, ProfileDancerUpdateCommand comand_)
        {

            profile.DanceRolId = comand_.DanceRolId  ??  profile.DanceRolId;
            profile.DanceLevelId = comand_.DanceLevelId ??  profile.DanceLevelId;
            return profile;
        }


    }

}

