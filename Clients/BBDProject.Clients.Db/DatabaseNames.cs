namespace BBDProject.Clients.Db
{
    static class DatabaseNames
    {
        private const string Prefix = "bbd_clients_";
     
        public const string UsersSchemaName = Prefix + "users";
        public const string RoleTableName = Prefix + "role";
        public const string UserTokenTableName = Prefix + "user_token";
        public const string UserTableName = Prefix + "user";
        public const string RoleClaimTableName = Prefix + "role_claim";
        public const string UserClaimTableName = Prefix + "user_claim";
        public const string UserLoginTableName = Prefix + "user_login";
        public const string UserRoleTableName = Prefix + "user_role";
    }
}
