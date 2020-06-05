namespace BBDProject.Management.Db
{
    static class DatabaseNames
    {
        private const string Prefix = "bbd_management_";

        public const string UsersSchemaName = Prefix + "employees";
        public const string RoleTableName = Prefix + "role";
        public const string UserTokenTableName = Prefix + "employee_token";
        public const string UserTableName = Prefix + "employee";
        public const string RoleClaimTableName = Prefix + "role_claim";
        public const string UserClaimTableName = Prefix + "employee_claim";
        public const string UserLoginTableName = Prefix + "employee_login";
        public const string UserRoleTableName = Prefix + "employee_role";
    }
}
