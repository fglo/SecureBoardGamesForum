using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0008)]
    public class AddForumTopicTable : Migration
    {
        private string _tableName = "forum_topic";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_author").AsInt32().NotNullable()
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("date_modified").AsDateTime().Nullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("deleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Create.ForeignKey("FK_user_forum_topic")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_author")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");
        }
    }
}
