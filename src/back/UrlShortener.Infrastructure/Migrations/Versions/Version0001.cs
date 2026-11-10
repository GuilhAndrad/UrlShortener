using FluentMigrator;

namespace UrlShortener.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.Add_ShortUrls_Table, "Creating ShortUrls table")]
public class Version0001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("ShortUrls")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Url").AsString(2048).NotNullable()
            .WithColumn("ShortCode").AsString(10).NotNullable().Unique()
            .WithColumn("AccessCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().NotNullable();
    }
}