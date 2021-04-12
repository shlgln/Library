using FluentMigrator;
using System.Data;

namespace Library.Migrations.Migrations
{
    [Migration(202104121845)]
    public class _202104211845_SchemaInitialized : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("BookCategories")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(100).NotNullable();

            Create.Table("Books")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("BookCategoryId").AsInt32().NotNullable()
                .ForeignKey("FK_Books_BookCategories", "BookCategories", "Id")
                .OnDelete(Rule.None)
                .WithColumn("Title").AsString(200).NotNullable()
                .WithColumn("Author").AsString(100).NotNullable()
                .WithColumn("MinimumAge").AsByte().NotNullable();

            Create.Table("Persons")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FullName").AsString(200).NotNullable()
                .WithColumn("Age").AsByte().NotNullable()
                .WithColumn("Address").AsString(400).NotNullable();

            Create.Table("Entrusts")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("BookId").AsInt32().NotNullable()
                .ForeignKey("FK_Entrusts_Books", "Books", "Id")
                .OnDelete(Rule.None)
                .WithColumn("PersonId").AsInt32().NotNullable()
                .ForeignKey("FK_Entrusts_Persons", "Persons", "Id")
                .OnDelete(Rule.None)
                .WithColumn("ReturnDate").AsDateTime2().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Entrusts");
            Delete.Table("Persons");
            Delete.Table("Books");
            Delete.Table("BookCategories");
        }


    }
}
