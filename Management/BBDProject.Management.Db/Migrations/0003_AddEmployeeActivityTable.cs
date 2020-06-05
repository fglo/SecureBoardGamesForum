using FluentMigrator;

namespace BBDProject.Management.Db.Migrations
{
    [Migration(0003)]
    public class AddemployeeActivityTable : Migration
    {
        private string _tableName = "employee_activity";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_employee").AsInt32().NotNullable()
                .WithColumn("id_activity").AsInt32().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable();

            Create.ForeignKey("FK_employee_employee_activity")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_employee")
                .ToTable(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");

            Create.ForeignKey("FK_activity_employee_activity")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_activity")
                .ToTable("activity").InSchema("public").PrimaryColumn("id");
        }
    }
}
