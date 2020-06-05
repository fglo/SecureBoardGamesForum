using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0009)]
    public class AddForumPostTable : Migration
    {
        private string _tableName = "forum_post";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_author").AsInt32().NotNullable()
                .WithColumn("id_forum_topic").AsInt32().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("date_modified").AsDateTime().Nullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("deleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Create.ForeignKey("FK_user_forum_post")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_author")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");

            Create.ForeignKey("FK_forum_topic_forum_post")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_forum_topic")
                .ToTable("forum_topic").InSchema("public").PrimaryColumn("id");
        }
    }
}
