using ServiceEventHandler.Command.CreateCommand;
using Utils;

namespace Abrazos.ServicesEvenetHandler.Intefaces
{
    public interface IInscriptionsCommandService
    {
        public Task<ResultApp> AddInscriptionAsync(InscriptionCommandCreate inscription);
        public Task<ResultApp> CancelInscriptionAsync(int userIncriptionId, int userId);

    }
}

