

using Abrazos.Persistence.Database;
using Abrazos.Services.Dto;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command;
using ServiceEventHandler.Command.CreateCommand;
using System.Data.Entity;
using System.Net.NetworkInformation;
using Utils;

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
            ResultApp res = new ResultApp();
            try
            {
                var resProfile = await this.command.Add<ProfileDancer>(MapToCreateEntity(command));
                User user = _dbContext.User.FirstOrDefault(u => u.UserId == command.UserId);
                res.Succeeded = resProfile != null ? true: false;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }

            return res;

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
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }

            return res;

        }

        public async Task<ResultApp> Delete(int profileDancerId)
        {
            ResultApp res = new ResultApp();
            try
            {
                ProfileDancer profile = _dbContext.ProfileDancer.SingleOrDefault(u => u.ProfileDanceId == profileDancerId);

                if (profile != null)
                {

                    await this.command.Delete<ProfileDancer>(profileDancerId);
                    res.Succeeded = true;
                }
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;

        }

        public ProfileDancer MapToUpdateEntity(ProfileDancer profile, ProfileDancerUpdateCommand comand_)
        {

            profile.DanceRolId = comand_.DanceRolId  ??  profile.DanceRolId;
            profile.DanceLevelId = comand_.DanceLevelId ??  profile.DanceLevelId;
            profile.Height = comand_.Height.HasValue ? comand_.Height : profile.Height;
            return profile;
        }

        public ProfileDancer MapToCreateEntity(ProfileDancerCreateCommand comand_)
        {
            ProfileDancer entity = new ProfileDancer();
            entity.DanceRolId = comand_.DanceRolId;
            entity.DanceLevelId = comand_.DanceLevelId;
            entity.UserId = comand_.UserId;
            entity.Height = comand_.Height?? null;
            return entity;
        }


    }

}

