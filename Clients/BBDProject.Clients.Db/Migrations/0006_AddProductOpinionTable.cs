using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0006)]
    public class AddProductOpinionTable : Migration
    {
        private string _tableName = "product_opinion";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_product").AsInt32().NotNullable()
                .WithColumn("id_user").AsInt32().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("stars").AsInt32().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("date_modified").AsDateTime().NotNullable();

            Create.ForeignKey("FK_product_product_opinion")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_product")
                .ToTable("product").InSchema("public").PrimaryColumn("id");

            Create.ForeignKey("FK_user_product_opinion")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_user")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");
        }
    }
}
