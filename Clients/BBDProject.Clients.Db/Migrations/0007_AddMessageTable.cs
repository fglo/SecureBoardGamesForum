using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0007)]
    public class AddMessageTable : Migration
    {
        private string _tableName = "message";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_author").AsInt32().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable();

            Create.ForeignKey("FK_user_message")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_author")
                .ToTable("user").InSchema("users").PrimaryColumn("Id");
        }
    }
}
