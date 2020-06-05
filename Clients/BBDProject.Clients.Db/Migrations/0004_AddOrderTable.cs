using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0004)]
    public class AddActivityTable : Migration
    {
        private string _tableName = "order";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_user").AsInt32().NotNullable()
                .WithColumn("invoice").AsBoolean().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable();

            Create.ForeignKey("FK_user_order")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_user")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");
        }
    }
}
