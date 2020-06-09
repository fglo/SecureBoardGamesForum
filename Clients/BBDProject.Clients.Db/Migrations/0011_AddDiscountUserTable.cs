using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0011)]
    public class AddDiscountUserTable : Migration
    {
        private string _tableName = "discount_user";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_user").AsInt32().NotNullable()
                .WithColumn("id_discount").AsInt32().NotNullable()
                .WithColumn("id_order").AsInt32().NotNullable();

            Create.ForeignKey("FK_user_discount_user")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_user")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");

            Create.ForeignKey("FK_discount_discount_user")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_discount")
                .ToTable("discount").InSchema("public").PrimaryColumn("id");

            Create.ForeignKey("FK_oder_discount_user")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_order")
                .ToTable("order").InSchema("public").PrimaryColumn("id");
        }
    }
}
