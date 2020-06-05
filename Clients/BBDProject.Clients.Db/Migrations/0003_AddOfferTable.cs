using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0003)]
    public class AddOfferTable : Migration
    {
        private string _tableName = "offer";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_user").AsInt32().NotNullable()
                .WithColumn("id_product").AsInt32().NotNullable()
                .WithColumn("number").AsInt32().NotNullable()
                .WithColumn("price").AsDouble().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("date_modified").AsDateTime().NotNullable()
                .WithColumn("deleted").AsBoolean().NotNullable();

            Create.ForeignKey("FK_user_offer")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_user")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");

            Create.ForeignKey("FK_product_offer")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_product")
                .ToTable("product").InSchema("public").PrimaryColumn("id");
        }
    }
}
