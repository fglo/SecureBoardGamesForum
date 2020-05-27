using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0005)]
    public class AddOderPositionTable : Migration
    {
        private string _tableName = "order_position";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_order").AsInt32().NotNullable()
                .WithColumn("id_offer").AsInt32().NotNullable()
                .WithColumn("number").AsInt32().NotNullable()
                .WithColumn("price").AsDouble().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("date_modified").AsDateTime().NotNullable()
                .WithColumn("deleted").AsBoolean().NotNullable();

            Create.ForeignKey("FK_order_order_position")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_order")
                .ToTable("order").InSchema("public").PrimaryColumn("id");

            Create.ForeignKey("FK_offer_order_position")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_offer")
                .ToTable("offer").InSchema("public").PrimaryColumn("id");
        }
    }
}
