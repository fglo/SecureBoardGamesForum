using Microsoft.AspNetCore.Http;

namespace BBDProject.Shared.Utils.Extensions
{
    public static class SessionExtensions
    {
        public static void SetLastPageNumber(this ISession session, int companyId)
        {
            session.SetInt32("LastPageNumber", companyId);
        }

        public static int GetLastPageNumber(this ISession session)
        {
            return session.GetInt32("LastPageNumber") ?? -1;
        }

        public static void SetLastMessageId(this ISession session, int messageId)
        {
            session.SetInt32("LastMessageId", messageId);
        }

        public static int GetLastMessageId(this ISession session)
        {
            return session.GetInt32("LastMessageId") ?? -1;
        }
    }
}
