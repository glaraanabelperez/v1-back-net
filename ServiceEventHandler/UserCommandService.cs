

using Abrazos.Persistence.Database;
using Abrazos.Services.Dto;
using Abrazos.ServicesEvenetHandler.Intefaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using ServiceEventHandler.Command;
using ServiceEventHandler.Command.CreateCommand;
using ServiceEventHandler.Command.UpdateCommand;
using ServicesQueries.Dto;
using System.Data.Entity;
using System.Net.NetworkInformation;
using Utils;

namespace Abrazos.ServiceEventHandler
{
    public class UserCommandService: IUserCommandService
    {
        private readonly ILogger<UserCommandService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public IGenericRepository command;

        public UserCommandService(ApplicationDbContext dbContext, IGenericRepository command, 
            ILogger<UserCommandService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            this.command = command;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultApp> AddUser(UserCreateCommand entity)
        {

            ResultApp res = new ResultApp();
            try
            {
                var user_res = await this.command.Add<User>(MapToUserEntity(entity));
                //alguna verificacion ok?
                res.Succeeded = true;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }

            return res;

        }

        public async Task<ResultApp> UpdateUser(UserUpdateCommand command)
        {
            ResultApp res = new ResultApp();

            var result = _dbContext.User
                .FirstOrDefault(u => u.UserId == command.userId);
            if (result != null)
            {
                var user_res = await this.command.Update<User>(MapToUserEntityInUpdate(result, command));
                res.Succeeded = true;
                res.message = "Update Successfull";

            }
            return res;

        }

        public User MapToUserEntityInUpdate(User user, UserUpdateCommand userCommand)
        {
            user.UserName = (userCommand.UserName != null && userCommand.UserName != string.Empty) ? userCommand.UserName : user.UserName;
            user.Email = (userCommand.Email != null && userCommand.Email != string.Empty) ? userCommand.Email : user.Email;
            user.Celphone = ( userCommand.Celphone != null && userCommand.Celphone != string.Empty) ? userCommand.Celphone : user.Celphone;
            user.Age = userCommand.Age != 0 ? userCommand.Age : user.Age;
            user.AvatarImage = (userCommand.AvatarImage != null && userCommand.AvatarImage != string.Empty) ? userCommand.AvatarImage : user.AvatarImage;
            user.LastName = (userCommand.LastName != null && userCommand.LastName != string.Empty )? userCommand.LastName : user.LastName;
            user.Name = (userCommand.Name != null && userCommand.Name != string.Empty) ? userCommand.Name : user.Name;
            user.UserIdFirebase = (userCommand.UserIdFirebase != null && userCommand.UserIdFirebase != string.Empty )? userCommand.UserIdFirebase : user.UserIdFirebase;

            return user;
        }

        public User MapToUserEntity(UserCreateCommand entity_)
        {
            User user = new User();
            user.UserName = entity_.UserName;
            user.Email = entity_.Email;
            user.Celphone = entity_.Celphone;
            user.Age = entity_.Age;
            user.AvatarImage = entity_.AvatarImage;
            user.LastName = entity_.LastName;
            user.Name = entity_.Name;
            user.UserIdFirebase = entity_.UserIdFirebase;
            user.UserState = true; //datos en appsetting
   
            return user;
        }

 
    }
}

