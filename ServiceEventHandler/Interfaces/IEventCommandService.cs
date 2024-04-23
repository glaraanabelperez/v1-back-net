using Abrazos.Services.Dto;
using Models;
using ServiceEventHandler.Command;
using ServiceEventHandler.Command.CreateCommand;
using System;
using Utils;

namespace Abrazos.ServicesEvenetHandler.Intefaces
{
    public interface IEventCommandService
    {
        public Task<ResultApp> Add(EventCreateCommand entity);
        public Task<ResultApp> Update(EventUpdateCommand entity);
        public Task<ResultApp> UpdateCupo(int eventiId, int cupo);

    }
}

