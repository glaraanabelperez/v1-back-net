using Utils;

namespace Abrazos.ServicesEvenetHandler.Intefaces
{
    public interface ICoupleCommandService
    {
        public Task<ResultApp> SendInvite(int userHost, int userInvited, int eventId);
        public Task<ResultApp> RespondInvite(int CouplesEventId, int userId, bool respond);
        public Task<ResultApp> CalcelCouple(int CouplesEventId, int userId);

    }
}

