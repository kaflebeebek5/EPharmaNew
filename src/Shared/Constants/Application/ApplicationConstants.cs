namespace EPharma.Shared.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class SignalR
        {
            public const string HubUrl = "/signalRHub";
            public const string SendUpdateDashboard = "UpdateDashboardAsync";
            public const string ReceiveUpdateDashboard = "UpdateDashboard";
            public const string SendRegenerateTokens = "RegenerateTokensAsync";
            public const string ReceiveRegenerateTokens = "RegenerateTokens";
            public const string ReceiveChatNotification = "ReceiveChatNotification";
            public const string SendChatNotification = "ChatNotificationAsync";
            public const string ReceiveMessage = "ReceiveMessage";
            public const string SendMessage = "SendMessageAsync";

            public const string OnConnect = "OnConnectAsync";
            public const string ConnectUser = "ConnectUser";
            public const string OnDisconnect = "OnDisconnectAsync";
            public const string DisconnectUser = "DisconnectUser";
            public const string OnChangeRolePermissions = "OnChangeRolePermissions";
            public const string LogoutUsersByRole = "LogoutUsersByRole";
        }
        public static class Cache
        {
            public const string GetAllGendersCacheKey = "all-genders";
            public const string GetAllCastesCacheKey = "all-castes";
            public const string GetAllCommunitiesCacheKey = "all-communities";
            public const string GetAllCategoriesCacheKey = "all-categories";
            public const string GetAllDocumentTypesCacheKey = "all-document-types";
            public const string GetAllHolidayTypesCacheKey = "all-holiday-types";
            public const string GetAllHolidayForCacheKey = "all-holiday-for";
            public const string GetAllHolidaySetupCacheKey = "all-holiday-setup";
            public const string GetAllCalenderCacheKey = "all-calender";
            public const string GetAllHRMenuRoleCacheKey = "all-MenuRole";
            public const string GetAllBankSetupCacheKey = "all-bank-setup";
            public const string GetAllStaffSetupCacheKey = "all-StaffSetup";

            public static string GetAllEntityExtendedAttributesCacheKey(string entityFullName)
            {
                return $"all-{entityFullName}-extended-attributes";
            }

            public static string GetAllEntityExtendedAttributesByEntityIdCacheKey<TEntityId>(string entityFullName, TEntityId entityId)
            {
                return $"all-{entityFullName}-extended-attributes-{entityId}";
            }
        }

        public static class MimeTypes
        {
            public const string OpenXml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}