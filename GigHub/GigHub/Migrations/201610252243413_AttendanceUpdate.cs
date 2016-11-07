namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendanceUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Artist_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Attendances", "Genre_GenreId", c => c.Byte());
            CreateIndex("dbo.Attendances", "Artist_Id");
            CreateIndex("dbo.Attendances", "Genre_GenreId");
            AddForeignKey("dbo.Attendances", "Artist_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Attendances", "Genre_GenreId", "dbo.Genres", "GenreId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Genre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.Attendances", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "Genre_GenreId" });
            DropIndex("dbo.Attendances", new[] { "Artist_Id" });
            DropColumn("dbo.Attendances", "Genre_GenreId");
            DropColumn("dbo.Attendances", "Artist_Id");
        }
    }
}
