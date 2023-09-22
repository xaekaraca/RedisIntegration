using RedisIntegration.Models;

namespace RedisIntegration.Mapper;

public static class SessionMapper
{
    public static SessionModel ToSessionModel(UserModel user, string sessionKey)
    {
        return new SessionModel
        {
            SessionKey = sessionKey,
            InformationId = user.InformationId,
            UserId = user.Id,
            Roles  = user.Roles ?? new List<string>(),
            CreatedAt = DateTime.UtcNow,
            Expiration = DateTime.UtcNow.AddMinutes(15)
        };
    }
    
    public static SessionModel ToSessionModel(SessionUpdateModel sessionUpdateModel, SessionModel session)
    {
        session.InformationId = sessionUpdateModel.InformationId;
        session.Roles = sessionUpdateModel.Roles ?? session.Roles;
        session.RefreshedAt = DateTime.UtcNow;
        session.Expiration = DateTime.UtcNow.AddMinutes(15);
        return session;
    }
}