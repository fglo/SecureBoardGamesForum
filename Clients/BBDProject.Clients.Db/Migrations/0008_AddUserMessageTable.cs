using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0008)]
    public class AddUserMessageTable : Migration
    {
        private string _tableName = "user_message";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id_user").AsInt32().PrimaryKey($"PK_{_tableName}").NotNullable()
                .WithColumn("id_message").AsInt32().NotNullable();

            Create.ForeignKey("FK_user_user_message")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_user")
                .ToTable("user").InSchema("users").PrimaryColumn("Id");

            Create.ForeignKey("FK_message_user_message")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_message")
                .ToTable("message").InSchema("public").PrimaryColumn("id");
        }
    }
}
